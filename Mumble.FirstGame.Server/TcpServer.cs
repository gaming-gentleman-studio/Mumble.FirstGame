using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Scene;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Server
{
    public class TcpServer : SocketListener
    {
        private State _state = new State();


        private AsyncCallback recv = null;

        private ConcurrentBag<Socket> _clients = new ConcurrentBag<Socket>();
        public TcpServer(IPEndPoint endpoint,IScene scene) : base(endpoint, scene)
        {

        }



        protected override void BindSocket(IPEndPoint endpoint)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(endpoint);
        }
        //public override void Listen()
        //{
        //    _socket.Listen(5);
        //    State state = new State();
        //    while (true)
        //    {
                
        //        Socket confirmed = _socket.Accept();
        //        _sender = confirmed.RemoteEndPoint;
        //        _clients.Add(confirmed);
        //        int bytes = confirmed.Receive(state.Buffer);
        //        Console.WriteLine("{0} bytes received", bytes);
        //        byte[] message = state.Buffer.Take(bytes).ToArray();
        //        AddToActionBuffer(bytes, message);
        //        CalculateAndReply(0,confirmed);
        //    }
        //}
        public override void Listen()
        {
            _socket.Listen(100);
            _socket.BeginAccept(new AsyncCallback(AcceptCallback), _state);
        }
        private void AcceptCallback(IAsyncResult ar)
        {
            State state = (State)ar.AsyncState;
            Socket handler = _socket.EndAccept(ar);
            state.workSocket = handler;
            _clients.Add(handler);
            handler.BeginReceive(state.Buffer, 0, _bufSize, SocketFlags.None, new AsyncCallback(ReadCallback), _state);
        }
        private void ReadCallback(IAsyncResult ar)
        {
            State state = (State)ar.AsyncState;
            Socket handler = state.workSocket;
            
            int bytes = handler.EndReceive(ar);
            byte[] message = state.Buffer.Take(bytes).ToArray();
            AddToActionBuffer(bytes, message);
            SendNull(handler, SocketScope.Private);
        }
        public void StartUpdateTask(int numTicks)
        {
            //need to handle issue with action buffer not being private
            //basically need to implement private buffer
            CalculateAndReply(numTicks, _socket,SocketScope.Private);
        }
        public void SendResultsToAllClients(List<IActionResult> results)
        {
            foreach(Socket client in _clients)
            {
                SendResults(results, client, SocketScope.Shared);
            }
            
        }
    }
}
