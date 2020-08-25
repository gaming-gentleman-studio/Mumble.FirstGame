using Mumble.FirstGame.Core;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Meta;
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
        public IScene CurrentScene { get; private set; }
        private Director _director;
        private int _tickRate = 1;
        public IOwnerIdentifier Owner { get; private set; }

        public SoloGameClient()
        {
            Owner = new IntOwnerIdentifier(1);
            _director = new Director();
            CurrentScene = _director.CurrentScene;
        }
        public List<IActionResult> Init()
        {
            EnterSceneAction action = new EnterSceneAction();
            Dictionary<IOwnerIdentifier, List<IAction>> ownedActions = new Dictionary<IOwnerIdentifier, List<IAction>>();
            ownedActions.Add(Owner, new List<IAction> { action });
            return CurrentScene.Update(ownedActions, 0);
        }
        public void CheckForSceneUpdate()
        {
            CurrentScene = _director.CurrentScene;
        }
        public void Register()
        {
            return;
        }

        public List<IActionResult> Update(List<IAction> actions)
        {
            Dictionary<IOwnerIdentifier, List<IAction>> ownedActions = new Dictionary<IOwnerIdentifier, List<IAction>>();
            ownedActions.Add(Owner, actions);
            return CurrentScene.Update(ownedActions, _tickRate);
        }
    }
}
