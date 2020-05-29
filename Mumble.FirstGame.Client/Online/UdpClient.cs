using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Client.Online
{
    public class UdpClient : SocketSender
    {
        public UdpClient(IPEndPoint endpoint) : base(endpoint)
        {
            
        }
        protected override void BindSocket(IPEndPoint endpoint)
        {
            
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(endpoint);
        }
        public void Listen(IEntityContainer container)
        {
            Receive(container);
        }
    }
}
