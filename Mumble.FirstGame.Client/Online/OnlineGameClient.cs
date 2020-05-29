using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mumble.FirstGame.Client.Online
{
    public class OnlineGameClient : IGameClient
    {
        private UdpClient _udpClient;
        private TcpClient _tcpClient;
        private IEntityContainer _entityContainer;
        public OnlineGameClient()
        {
            IPEndPoint tcpEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27000);
            _tcpClient = new TcpClient(tcpEndpoint);
            IPEndPoint udpEndpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27000);
            _udpClient = new UdpClient(udpEndpoint);
        }

        public void Send(IAction action)
        {
            _udpClient.Send(action, _entityContainer);
        }
        
        public List<IActionResult> Update(List<IAction> actions)
        {
            return _udpClient.Update(actions, _entityContainer);
        }

        public List<IActionResult> Init()
        {
            _entityContainer = new BattleEntityContainer();
            SpawnPlayerAction spawnAction = new SpawnPlayerAction("beau", 3, 10);
            _tcpClient.Send(spawnAction, _entityContainer);
            _udpClient.Listen(_entityContainer)
            return new List<IActionResult>();
            
        }

    }
}
