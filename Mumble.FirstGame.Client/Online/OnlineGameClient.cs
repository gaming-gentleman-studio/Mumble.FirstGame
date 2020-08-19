using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Meta;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.MonogameShared.Settings;
using Mumble.FirstGame.Serialization.OnlineAction;
using Mumble.FirstGame.Serialization.OnlineActionResult;
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
        private IntOwnerIdentifier _identifier;

        public IScene CurrentScene => throw new NotImplementedException(); //TODO

        public IOwnerIdentifier Owner { get; private set; }

        public OnlineGameClient(IEntityContainer container, IGameSettings settings, ISerializationFactoryContainer factoryContainer)
        {
            _tcpClient = new TcpClient(settings.Server, factoryContainer);
            _udpClient = new UdpClient(settings.Server, factoryContainer);
            _entityContainer = container;
        }

        public void Send(IAction action)
        {
            _udpClient.Send(_identifier,action, _entityContainer);
        }
        
        public List<IActionResult> Update(List<IAction> actions)
        {
            List<IActionResult> results = _udpClient.Update(_identifier, actions, _entityContainer);
            results.AddRange(_tcpClient.GetNewResults());
            return results;
        }

        public List<IActionResult> Init()
        {

            // some of these fields don't actually matter - just need to tell server where we are spawning
            EnterSceneAction enterSceneAction = new EnterSceneAction();
            List<IActionResult> results =_tcpClient.Send(_identifier, enterSceneAction, _entityContainer);
            _udpClient.Listen();
            _tcpClient.Listen();
            return results;
            
        }

        public void Register()
        {
            RegisterClientAction registerAction = new RegisterClientAction();
            IntOwnerIdentifier tempIdentifier = new IntOwnerIdentifier(0);
            List<IActionResult> results = _tcpClient.Send(tempIdentifier,registerAction, _entityContainer);
            ClientRegisteredActionResult result = results.Where(x => x is ClientRegisteredActionResult).Cast<ClientRegisteredActionResult>().FirstOrDefault();
            _identifier = (IntOwnerIdentifier)result.OwnerIdentifier; 
            Owner = result.OwnerIdentifier;
        }
    }
}
