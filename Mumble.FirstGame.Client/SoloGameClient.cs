using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Client
{
    public class SoloGameClient : IGameClient
    {
        private IScene _scene;
        private Player _player;
        private List<ICombatAIEntity> _enemies;

        public List<ICombatAIEntity> GetEnemies()
        {
            return new List<ICombatAIEntity>();
        }

        public List<Player> GetPlayers()
        {
            return new List<Player>() { _player };
        }

        public void Init(IEntityContainer entityContainer)
        {
            SceneBoundary boundary = new SceneBoundary(100, 100);
            _scene = new BattleScene((BattleEntityContainer) entityContainer, boundary);
        }

        public List<IActionResult> Update(List<IAction> actions)
        {
            return _scene.Update(actions);
        }
    }
}
