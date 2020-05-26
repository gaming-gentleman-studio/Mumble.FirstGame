using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.ActionResult;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Server
{
    public class UdpServer
    {
        private Socket _socket;
        private const int _bufSize = 8 * 1024;
        private State _state = new State();
        private EndPoint _sender = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;
        private IActionFactory _actionFactory;
        private IScene _scene;
        private IEntityContainer _entityContainer;

        public class State
        {
            public byte[] Buffer = new byte[_bufSize];
        }
        public UdpServer(IPEndPoint endpoint)
        {
            //TODO - move out
            Player player = new Player(3, 10);
            _entityContainer = new BattleEntityContainer(new List<Core.Entity.IMoveableCombatEntity>() { player });
            _actionFactory = new ActionFactory(_entityContainer);
            _scene = new BattleScene((BattleEntityContainer) _entityContainer, new SceneBoundary(100, 100));
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(endpoint);
        }
        public void Listen()
        {
            _socket.BeginReceiveFrom(_state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv = (ar) =>
               {
                   State state = (State)ar.AsyncState;
                   int bytes = _socket.EndReceiveFrom(ar, ref _sender);
                   _socket.BeginReceiveFrom(state.Buffer, 0, _bufSize, SocketFlags.None, ref _sender, recv, state);
                   IAction action= _actionFactory.Create(state.Buffer.Take(bytes).ToArray());
                   List<IActionResult> results = _scene.Update(new List<IAction>() { action });
                   SendResponse(results);
                   Console.WriteLine("RECV: {0}: {1}, {2}", _sender.ToString(), bytes, Encoding.ASCII.GetString(state.Buffer, 0, bytes));
                   
               }, _state);
        }
        private void SendResponse(List<IActionResult> results)
        {
            foreach (IActionResult result in results)
            {
                //TODO - inefficient
                byte[] untypedData = result.ToProtobufDefinition(_entityContainer).ToByteArray();
                byte[] data = new byte[untypedData.Length + 1];
                data[0] = result.GetTypeByte();
                Array.Copy(untypedData, 0, data, 1, untypedData.Length);
                _socket.SendTo(data, _sender);
            }
                
        }
    }
}
