using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using System.Collections.Generic;

namespace Mumble.FirstGame.Client
{
    public class SoloGameClient : IGameClient
    {
        private IScene _scene;
        private int _tickRate = 1;


        public List<IActionResult> Init()
        {
            _scene = new BattleScene();
            SpawnPlayerAction action = new SpawnPlayerAction("beau",3,10);
            return _scene.Update(new List<IAction>() { action }, 0);
        }

        public List<IActionResult> Update(List<IAction> actions)
        {
            return _scene.Update(actions, _tickRate);
        }
    }
}
