using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
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
        protected Socket _socket;
        
        
        
        
        protected IActionFactory _actionFactory;
        protected IScene _scene;
        
        protected ConcurrentBag<IAction> _actionBuffer;
        protected EndPoint _sender = new IPEndPoint(IPAddress.Any, 0);
        protected const int _bufSize = 8 * 1024 * 2;

        public SocketListener(IPEndPoint endpoint,IScene scene)
        {

            _scene = scene;
            _actionFactory = new ActionFactory(_scene.EntityContainer);
            _actionBuffer = new ConcurrentBag<IAction>();
            BindSocket(endpoint);
        }
        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        protected abstract void BindSocket(IPEndPoint endpoint);
        public abstract void Listen();

        protected void AddToActionBuffer(int len,byte[] message)
        {
            if (message[0] == message.Length)
            {
                IAction action = _actionFactory.Create(message.Skip(1).ToArray());
                Console.WriteLine("RECV: {0}: {1}, {2}", _sender.ToString(), len, action.GetType().ToString());
                _actionBuffer.Add(action);
            }
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

        protected void SendNull()
        {
            _socket.SendTo(new byte[0],_sender);
        }
        protected void SendResults(List<IActionResult> results, Socket socket)
        {
            List<byte> data = new List<byte>();
            foreach (IActionResult result in results)
            {
                data.AddRange(GetResultBytes(result));
            }
            if (data.Count > 0)
            {
                socket.SendTo(data.ToArray(), _sender);
                Console.WriteLine("SENT: {0}: {1}", _sender.ToString(), data.Count);
            }
        }
        protected void CalculateAndReply(int numTicks,Socket socket)
        {
            List<IAction> actions;
            lock (_actionBuffer)
            {
                actions = new List<IAction>(_actionBuffer);
                _actionBuffer = new ConcurrentBag<IAction>();
            }
            List<IActionResult> results = new List<IActionResult>();
            results.AddRange(_scene.Update(new List<IAction>(actions), numTicks));
            SendResults(results, socket);
        }

    }
}
