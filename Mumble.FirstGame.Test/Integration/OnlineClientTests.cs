using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mumble.FirstGame.Client;
using Mumble.FirstGame.Client.Online;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.MonogameShared.Settings;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mumble.FirstGame.Test.Integration
{
    [TestClass]
    public class OnlineClientTests
    {
        private Player _player;
        private OnlineGameClient _client;
        [TestInitialize]
        public void Setup()
        {
            var container = new BattleEntityContainer();
            var settings = new Settings();
            var factoryContainer = new SerializationFactoryContainer(new ActionFactory(container), new ActionResultFactory(container), new EntityFactory());
            _client = new OnlineGameClient(container, settings, factoryContainer);
            _client.Register();
            var owner = new IntOwnerIdentifier(1);
            List<IActionResult> results = _client.Init(owner);
            EntitiesCreatedActionResult createdResult = (EntitiesCreatedActionResult)results.Where(x => x is EntitiesCreatedActionResult).FirstOrDefault();
            _player = (Player)createdResult.Entities.Where(x => x.OwnerIdentifier.Equals(owner)).FirstOrDefault();
        }

        [TestMethod]
        public void Test1()
        {
            MoveAction move = new MoveAction(_player, Direction.Right);
            FireWeaponAction fire = new FireWeaponAction(_player, Direction.Up);
            List<IAction> actions = new List<IAction>()
            {
                fire,
                move
                
            };
            List<IActionResult> results1 = _client.Update(actions);
            Thread.Sleep(50);
            List<IActionResult> results2 = _client.Update(actions);
            Thread.Sleep(50);
            List<IActionResult> results3 = _client.Update(actions);
            Thread.Sleep(50);
            List<IActionResult> results4 = _client.Update(actions);
            Assert.AreEqual(true, true);
        }
        private class Settings : IGameSettings
        {
            public IPEndPoint Server => new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27000);

            public ClientType ClientType => ClientType.Online;

            public TimeSpan TickRate => TimeSpan.FromMilliseconds(50);

            public bool FullScreen => false;
        }
    }
}
