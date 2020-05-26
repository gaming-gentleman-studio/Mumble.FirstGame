using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
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
        private const int _bufSize = 8 * 1024;
        private State _state = new State();
        private EndPoint _sender = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        private IEntityContainer _entityContainer;
        private IActionResultFactory _actionResultFactory;
        private List<IActionResult> _results;
        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        public OnlineGameClient(IPEndPoint endpoint)
        {
            _results = new List<IActionResult>();
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
                _results.Add(_actionResultFactory.Create(state.Buffer.Take(bytes).ToArray()));
                Debug.WriteLine("RECV: {0}: {1}, {2}", _sender.ToString(), bytes, Encoding.ASCII.GetString(state.Buffer, 0, bytes));
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
            byte[] data = new byte[untypedData.Length + 1];
            data[0] = action.GetTypeByte();                         
            Array.Copy(untypedData, 0, data, 1, untypedData.Length);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
               {
                   State state = (State)ar.AsyncState;
                   int bytes = _socket.EndSend(ar);
                   Debug.WriteLine("Trying to send...");
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
            if (actions.Count > 0)
            {
                foreach(IAction action in actions)
                {
                    Send(action);
                }
            }
            List<IActionResult> retList = _results;
            _results = new List<IActionResult>();
            return retList;
        }

        public void Init(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
            _actionResultFactory = new ActionResultFactory(entityContainer);
        }
    }
}
