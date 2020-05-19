using System;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace Mumble.FirstGame.Server
{
    public class UdpServer
    {
        private Socket _socket;
        private const int _bufSize = 8 * 1024;
        private State _state = new State();
        private EndPoint _validSender = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;

        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        public UdpServer(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(endpoint);
        }
        public void Listen()
        {
            _socket.BeginReceiveFrom(_state.Buffer, 0, _bufSize, SocketFlags.None, ref _validSender, recv = (ar) =>
               {
                   State state = (State)ar.AsyncState;
                   int bytes = _socket.EndReceiveFrom(ar, ref _validSender);
                   _socket.BeginReceiveFrom(state.Buffer, 0, _bufSize, SocketFlags.None, ref _validSender, recv, state);
                   Console.WriteLine("RECEIVED");
               }, _state);
        }
    }
}
