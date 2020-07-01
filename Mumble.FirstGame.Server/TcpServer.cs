﻿using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Server
{
    public class TcpServer : SocketListener
    {
        private State _state = new State();


        private ConcurrentBag<Socket> _clients = new ConcurrentBag<Socket>();
        public TcpServer(IPEndPoint endpoint,IScene scene, IFactoryContainer factoryContainer) : base(endpoint, scene, factoryContainer)
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
            _socket.Listen(100);
            _socket.BeginAccept(new AsyncCallback(AcceptCallback), _state);
        }
        private void AcceptCallback(IAsyncResult ar)
        {
            State state = (State)ar.AsyncState;
            Socket handler = _socket.EndAccept(ar);
            state.workSocket = handler;
            _clients.Add(handler);
            handler.BeginReceive(state.Buffer, 0, _bufSize, SocketFlags.None, new AsyncCallback(ReadCallback), state);
            _socket.BeginAccept(new AsyncCallback(AcceptCallback), _state);
                        
        }
        private void ReadCallback(IAsyncResult ar)
        {
            State state = (State)ar.AsyncState;
            Socket handler = state.workSocket;
            try
            {
                int bytes = handler.EndReceive(ar);
                byte[] message = state.Buffer.Take(bytes).ToArray();
                AddToActionBuffer(bytes, message);
                SendNull(handler, SocketScope.Private);
                handler.BeginReceive(state.Buffer, 0, _bufSize, SocketFlags.None, new AsyncCallback(ReadCallback), state);
            }
            catch (SocketException ex)
            {
                lock (_clients)
                {
                    _clients = new ConcurrentBag<Socket>(_clients.Except(new[] { handler }));
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void StartUpdateTask(int numTicks)
        {
            List<IActionResult> results = CalculateResults(numTicks);
            foreach (Socket client in _clients)
            {
                SendResults(results, client, SocketScope.Private);
            }
            
        }
    }
}
