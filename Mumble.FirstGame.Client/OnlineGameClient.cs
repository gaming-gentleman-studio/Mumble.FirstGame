using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using System;
using System.Collections.Generic;
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
        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        public OnlineGameClient(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(endpoint);
            Receive();
        }
        public void Receive()
        {
            _socket.BeginReceiveFrom(_state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv = (ar) =>
            {
                State state = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref _sender);
                _socket.BeginReceiveFrom(state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv, state);
                Console.WriteLine("RECV: {0}: {1}, {2}", _sender.ToString(), bytes, Encoding.ASCII.GetString(state.Buffer, 0, bytes));
            }, _state);
        }
        public void Send(string text)
        {
            byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
               {
                   State state = (State)ar.AsyncState;
                   int bytes = _socket.EndSend(ar);
                   Console.WriteLine("Trying to send...");
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

        public List<IActionResult> Update(List<IAction> actions, TimeSpan elapsed)
        {
            if (actions.Count > 0)
            {
                Send("Update");
            }
            Send("Update");
            return new List<IActionResult>();
        }

        public void Init(Player player)
        {
            Send("HI");
        }
    }
}
