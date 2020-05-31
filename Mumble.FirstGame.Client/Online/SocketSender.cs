using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Client.Online
{
    public abstract class SocketSender
    {
        protected Socket _socket;
        private const int _bufSize = 8 * 1024 * 2;
        private State _state = new State();
        private EndPoint _sender = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        
        private IActionResultFactory _actionResultFactory;
        private ConcurrentBag<IActionResult> _resultBuffer;
        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        public SocketSender(IPEndPoint endpoint)
        {
            
            _resultBuffer = new ConcurrentBag<IActionResult>();
            BindSocket(endpoint);
            
        }
        protected abstract void BindSocket(IPEndPoint endpoint);
        protected void Receive(IEntityContainer entityContainer,bool async=true)
        {
            _actionResultFactory = new ActionResultFactory(entityContainer);
            if (async)
            {
                _socket.BeginReceiveFrom(_state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv = (ar) =>
                {
                    State state = (State)ar.AsyncState;
                    int bytes = _socket.EndReceiveFrom(ar, ref _sender);
                    _socket.BeginReceiveFrom(state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv, state);
                    ParseBytesReceived(state, bytes);

                }, _state);
            }
            else
            {
                State state = new State();
                int bytes = _socket.Receive(state.Buffer);
                ParseBytesReceived(state, bytes);
            }

        }
        private void ParseBytesReceived(State state, int bytes)
        {
            if (bytes > 0)
            {
                byte[] packet = state.Buffer.Take(bytes).ToArray();
                while (packet.Length > 0)
                {
                    byte[] message = packet.Take(packet[0]).ToArray();
                    IActionResult result = _actionResultFactory.Create(message.Skip(1).ToArray());
                    if (result != null)
                    {
                        _resultBuffer.Add(result);
                    }
                    packet = packet.Skip(packet[0]).ToArray();
                }
            }
        }
        protected void SendInternal(IAction action, IEntityContainer entityContainer, bool async=true)
        {
            if (action == null)
            {
                return;
            }
            //TODO - inefficient
            byte[] untypedData = action.ToProtobufDefinition(entityContainer).ToByteArray();
            byte[] data = new byte[untypedData.Length + 2];
            data[0] = (byte)(untypedData.Length + 2);
            data[1] = action.GetTypeByte();
            Array.Copy(untypedData, 0, data, 2, untypedData.Length);
            if (async)
            {
                _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
                {
                    State state = (State)ar.AsyncState;
                    int bytes = _socket.EndSend(ar);
                }, _state);
            }
            else
            {
                _socket.Send(data);
            }
        }
        protected List<IActionResult> ClearResultBuffer()
        {
            List<IActionResult> retList = new List<IActionResult>();
            lock (_resultBuffer)
            {
                retList.AddRange(_resultBuffer.ToList());
                _resultBuffer = new ConcurrentBag<IActionResult>();
            }
            return retList;

        }
        public List<IActionResult> Update(List<IAction> actions,IEntityContainer entityContainer)
        {
            entityContainer.HardDeleteEntities();
            List<IActionResult> retList = ClearResultBuffer();
            HashSet<IEntity> destroyed = new HashSet<IEntity>(retList.Where(x => x is EntityDestroyedActionResult).Select(x => ((EntityDestroyedActionResult)x).Entity));
            entityContainer.RemoveEntities(destroyed);
            if (actions.Count > 0)
            {
                foreach (IAction action in actions)
                {
                    SendInternal(action,entityContainer);
                }
            }
            return retList;
        }
    }
}
