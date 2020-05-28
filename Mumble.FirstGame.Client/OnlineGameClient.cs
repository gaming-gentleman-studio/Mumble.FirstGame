using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Client
{
    public class OnlineGameClient : IGameClient
    {
        private Socket _socket;
        private const int _bufSize = 8 * 1024 * 2;
        private State _state = new State();
        private EndPoint _sender = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        private IEntityContainer _entityContainer;
        private IActionResultFactory _actionResultFactory;
        private ConcurrentBag<IActionResult> _resultBuffer;
        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        public OnlineGameClient(IPEndPoint endpoint)
        {
            _resultBuffer = new ConcurrentBag<IActionResult>();
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(endpoint);
            Receive();
        }
        private void Receive()
        {
            _socket.BeginReceiveFrom(_state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv = (ar) =>
            {
                State state = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref _sender);
                _socket.BeginReceiveFrom(state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv, state);
                if (bytes > 0)
                {
                    byte[] message = state.Buffer.Take(bytes).ToArray();
                    if (message[0] == message.Length)
                    {
                        IActionResult result = _actionResultFactory.Create(message.Skip(1).ToArray());
                        if (result != null)
                        {
                            _resultBuffer.Add(result);
                        }
                        
                    }
                    
                }

            }, _state);
        }
        private void Send(IAction action)
        {
            if (action == null)
            {
                return;
            }
            //TODO - inefficient
            byte[] untypedData = action.ToProtobufDefinition(_entityContainer).ToByteArray();
            byte[] data = new byte[untypedData.Length + 2];
            data[0] = (byte)(untypedData.Length + 2);
            data[1] = action.GetTypeByte();                         
            Array.Copy(untypedData, 0, data, 2, untypedData.Length);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
               {
                   State state = (State)ar.AsyncState;
                   int bytes = _socket.EndSend(ar);
               }, _state);
        }
        
        public List<ICombatAIEntity> GetEnemies()
        {
            return new List<ICombatAIEntity>();
        }

        public List<Player> GetPlayers()
        {
            return new List<Player>(); 
        }
        public List<IActionResult> Update(List<IAction> actions)
        {
            _entityContainer.HardDeleteEntities();
            if (actions.Count > 0)
            {
                foreach(IAction action in actions)
                {
                    Send(action);
                }
            }
            List<IActionResult> retList = new List<IActionResult>();
            lock (_resultBuffer)
            {
                retList.AddRange(_resultBuffer.ToList());
                _resultBuffer = new ConcurrentBag<IActionResult>();
            }
            HashSet<IEntity> destroyed = new HashSet<IEntity>(retList.Where(x => x is EntityDestroyedActionResult).Select(x => ((EntityDestroyedActionResult)x).Entity));
            _entityContainer.RemoveEntities(destroyed);
            return retList;
        }

        public void Init(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
            _actionResultFactory = new ActionResultFactory(entityContainer);
        }

    }
}
