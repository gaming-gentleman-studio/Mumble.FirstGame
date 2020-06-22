using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf;
using Mumble.FirstGame.Serialization.Protobuf.ActionResult;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace Mumble.FirstGame.Server
{
    public abstract class SocketListener
    {
        public enum SocketScope
        {
            Shared,
            Private
        };
        protected Socket _socket;
        protected IActionFactory _actionFactory;
        protected IScene _scene;
        
        protected ConcurrentDictionary<IOwnerIdentifier,ConcurrentBag<IAction>> _actionBuffer;
        protected EndPoint _sender = new IPEndPoint(IPAddress.Any, 0);
        protected const int _bufSize = 8 * 1024 * 2;
        private static ConcurrentBag<IOwnerIdentifier> _ownerSet = new ConcurrentBag<IOwnerIdentifier>();
        private ConcurrentDictionary<IPEndPoint,IOwnerIdentifier> _ownerMap = new ConcurrentDictionary<IPEndPoint, IOwnerIdentifier>();
        private int _nextOwnerId = 1;

        public SocketListener(IPEndPoint endpoint,IScene scene)
        {
            _scene = scene;
            _actionFactory = new ActionFactory(_scene.EntityContainer);
            _actionBuffer = new ConcurrentDictionary<IOwnerIdentifier,ConcurrentBag<IAction>>();
            BindSocket(endpoint);
        }
        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
            public Socket workSocket = null;
        }
        protected abstract void BindSocket(IPEndPoint endpoint);
        public abstract void Listen();

        // byte array
        // array[0] = length
        // array[1] = identifier
        // below is repeated..
        // array[2] = action type
        // array[2-n] = message
        protected void AddToActionBuffer(int len,byte[] message)
        {
            if (message[0] == message.Length)
            {
                if (message[2] == ActionTypeLookup.ClientRegistration || IsAuthenticated(message))
                {
                    IntOwnerIdentifier identifier;
                    if (message[2] == ActionTypeLookup.ClientRegistration)
                    {
                        identifier = RegisterNewClient(message);
                    }
                    else
                    {
                        identifier = GetIdentifier(message);
                    }

                    IAction action = _actionFactory.Create(message.Skip(2).ToArray(), identifier);
                    Console.WriteLine("RECV: {0}: {1}, {2}", _sender.ToString(), len, action.GetType().ToString());
                    if (!_actionBuffer.ContainsKey(identifier))
                    {
                        if (!_actionBuffer.TryAdd(identifier, new ConcurrentBag<IAction>()))
                        {
                            throw new Exception("Unable to add new owner to buffer");
                        }
                    }
                    _actionBuffer[identifier].Add(action);
                }

            }
        }
        private bool IsAuthenticated(byte[] message)
        {
            IntOwnerIdentifier identifier = IntOwnerIdentifier.FromByte(message[1]);
            if (!_ownerSet.Contains(identifier))
            {
                return false;
            }
            _ownerMap.TryAdd((IPEndPoint)_sender, identifier);
            return true;
        }
        private IntOwnerIdentifier RegisterNewClient(byte[] message)
        {
            IntOwnerIdentifier identifier = new IntOwnerIdentifier(_nextOwnerId);
            _nextOwnerId++;
            _ownerSet.Add(identifier);
            _ownerMap.TryAdd((IPEndPoint)_sender, identifier);
            return identifier;
        }
        private IntOwnerIdentifier GetIdentifier(byte[] message)
        {
            IntOwnerIdentifier identifier;
            identifier = IntOwnerIdentifier.FromByte(message[1]);
            if (!_ownerSet.Contains(identifier))
            {
                throw new Exception("Identifier not found");
            }
            return identifier;
        }
        protected List<byte> GetResultBytes(IActionResult result)
        {
            byte[] untypedData = result.ToProtobufDefinition(_scene.EntityContainer).ToByteArray();
            byte[] data = new byte[untypedData.Length + 2];
            data[0] = (byte)(untypedData.Length + 2);
            data[1] = result.GetTypeByte();
            Array.Copy(untypedData, 0, data, 2, untypedData.Length);
            return data.ToList();
        }

        protected void SendNull(Socket socket,SocketScope scope)
        {
            if (scope == SocketScope.Shared)
            {
                socket.SendTo(new byte[0], _sender);
            }
            else if (socket.Connected)
            {
                socket.Send(new byte[0]);
            }
            
        }
        private void SendRaw(byte[] data,Socket socket,SocketScope scope)
        {
            if (scope == SocketScope.Shared)
            {
                foreach (IPEndPoint endpoint in _ownerMap.Keys)
                {
                    socket.SendTo(data, endpoint);
                    Console.WriteLine("SENT TO: {0}", endpoint.ToString());
                }
            }
            else if (socket.Connected)
            {
                socket.Send(data);
                Console.WriteLine("SENT TO: {0}", _sender.ToString());
            }
        }
        protected void SendResults(List<IActionResult> results, Socket socket, SocketScope scope)
        {
            List<byte> data = new List<byte>();
            foreach (IActionResult result in results)
            {
                data.AddRange(GetResultBytes(result));
                Console.WriteLine("SENT: {0}, {1}", data.Count,result.GetType().ToString());
            }
            if (data.Count > 0)
            {
                SendRaw(data.ToArray(),socket,scope);
            }
        }
        protected void CalculateAndReply(int numTicks, Socket socket, SocketScope scope)
        {
            List<IActionResult> results = CalculateResults(numTicks);
            if (results.Count > 0)
            {
                SendResults(results, socket, scope);
            }
            

        }
        protected List<IActionResult> CalculateResults(int numTicks)
        {
            Dictionary<IOwnerIdentifier, List<IAction>> actions = new Dictionary<IOwnerIdentifier, List<IAction>>();
            lock (_actionBuffer)
            {
                foreach (IOwnerIdentifier key in _actionBuffer.Keys)
                {
                    actions.Add(key, _actionBuffer[key].ToList());
                }
                _actionBuffer = new ConcurrentDictionary<IOwnerIdentifier, ConcurrentBag<IAction>>();
            }
            List<IActionResult> results = new List<IActionResult>();
            results.AddRange(_scene.Update(actions, numTicks));
            return results;
        }

    }
}
