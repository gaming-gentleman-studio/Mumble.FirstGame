using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Client.Online
{
    public class TcpClient : SocketSender
    {
        public TcpClient(IPEndPoint endpoint) : base(endpoint)
        {

        }
        protected override void BindSocket(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(endpoint);
        }
    }
}
