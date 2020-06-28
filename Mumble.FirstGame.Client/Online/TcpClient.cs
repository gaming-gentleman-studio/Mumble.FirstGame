﻿using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Client.Online
{
    public class TcpClient : SocketSender
    {
        public TcpClient(IPEndPoint endpoint, IActionResultFactory resultFactory) : base(endpoint, resultFactory)
        {

        }
        protected override void BindSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(Endpoint);
        }
        public List<IActionResult> Send(IntOwnerIdentifier identifier,IAction action, IEntityContainer entityContainer)
        {
            if (!_socket.Connected)
            {
                BindSocket();
            }
            
            base.SendInternal(identifier,action, entityContainer, false);
            Receive(false);
            return ClearResultBuffer();

        }
        public void Listen()
        {
            Receive();
        }
        public List<IActionResult> GetNewResults()
        {
            return ClearResultBuffer();
        }
    }
}