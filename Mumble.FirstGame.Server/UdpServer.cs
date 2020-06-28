using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace Mumble.FirstGame.Server
{
    public class UdpServer : SocketListener
    {
        private State _state = new State();

        
        private AsyncCallback recv = null;
        public UdpServer(IPEndPoint endpoint,IScene scene, IActionFactory actionFactory) : base(endpoint, scene, actionFactory)
        {

        }

        protected override void BindSocket(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(endpoint);
        }
        public override void Listen()
        {
            _socket.BeginReceiveFrom(_state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv = (ar) =>
            {
                State state = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref _sender);
                _socket.BeginReceiveFrom(state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv, state);

                byte[] message = state.Buffer.Take(bytes).ToArray();
                AddToActionBuffer(bytes, message);
                SendNull(_socket,SocketScope.Shared);
            }, _state);
        }
        public void StartUpdateTask(int numTicks)
        {
            CalculateAndReply(numTicks, _socket,SocketScope.Shared);
        }
    }
}
