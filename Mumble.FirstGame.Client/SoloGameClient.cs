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
        private IScene _scene;
        private int _tickRate = 1;
        private IOwnerIdentifier _ownerIdentifier;

        public SoloGameClient()
        {
            _ownerIdentifier = new IntOwnerIdentifier(1);
        }
        public List<IActionResult> Init(IOwnerIdentifier owner)
        {
            
            _scene = new BattleScene(new BattleEntityContainer(),new List<IActionAdapter>());
            SpawnPlayerAction action = new SpawnPlayerAction("beau",3,10, _ownerIdentifier);
            Dictionary<IOwnerIdentifier, List<IAction>> ownedActions = new Dictionary<IOwnerIdentifier, List<IAction>>();
            ownedActions.Add(_ownerIdentifier, new List<IAction> { action });
            return _scene.Update(ownedActions, 0);
        }

        public IOwnerIdentifier Register()
        {
            return _ownerIdentifier;
        }

        public List<IActionResult> Update(List<IAction> actions)
        {
            Dictionary<IOwnerIdentifier, List<IAction>> ownedActions = new Dictionary<IOwnerIdentifier, List<IAction>>();
            ownedActions.Add(_ownerIdentifier, actions);
            return _scene.Update(ownedActions, _tickRate);
        }
    }
}
