using Mumble.FirstGame.Core;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System.Collections.Generic;

namespace Mumble.FirstGame.Client
{
    public class SoloGameClient : IGameClient
    {
        public IScene CurrentScene => _director.CurrentScene;
        private Director _director;
        private int _tickRate = 1;
        private IOwnerIdentifier _ownerIdentifier;

        public SoloGameClient()
        {
            _ownerIdentifier = new IntOwnerIdentifier(1);
            _director = new Director();
        }
        public List<IActionResult> Init(IOwnerIdentifier owner)
        {
            
            SpawnPlayerAction action = new SpawnPlayerAction("beau",3,10, _ownerIdentifier);
            Dictionary<IOwnerIdentifier, List<IAction>> ownedActions = new Dictionary<IOwnerIdentifier, List<IAction>>();
            ownedActions.Add(_ownerIdentifier, new List<IAction> { action });
            return CurrentScene.Update(ownedActions, 0);
        }

        public IOwnerIdentifier Register()
        {
            return _ownerIdentifier;
        }

        public List<IActionResult> Update(List<IAction> actions)
        {
            Dictionary<IOwnerIdentifier, List<IAction>> ownedActions = new Dictionary<IOwnerIdentifier, List<IAction>>();
            ownedActions.Add(_ownerIdentifier, actions);
            return CurrentScene.Update(ownedActions, _tickRate);
        }
    }
}
