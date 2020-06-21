using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
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
        protected override void BindSocket()
        {
            
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Connect(Endpoint);
        }
        public void Listen(IEntityContainer container)
        {
            Receive(container);
        }
        public void Send(IntOwnerIdentifier identifier,IAction action,IEntityContainer container)
        {
            SendInternal(identifier,action, container);
        }
    }
}
