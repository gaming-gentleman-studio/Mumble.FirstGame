using Mumble.FirstGame.Core.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Server
{
    public class TcpServer : SocketListener
    {
        public TcpServer(IPEndPoint endpoint,IScene scene) : base(endpoint, scene)
        {
        }



        protected override void BindSocket(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(endpoint);
        }
        public override void Listen()
        {
            _socket.Listen(5);
            State state = new State();
            while (true)
            {
                Socket confirmed = _socket.Accept();
                _sender = confirmed.RemoteEndPoint;
                int bytes = confirmed.Receive(state.Buffer);
                Console.WriteLine("{0} bytes received", bytes);
                byte[] message = state.Buffer.Take(bytes).ToArray();
                AddToActionBuffer(bytes, message);
                CalculateAndReply(0,confirmed);
                confirmed.Shutdown(SocketShutdown.Both);
                confirmed.Close();
            }
        }
    }
}
