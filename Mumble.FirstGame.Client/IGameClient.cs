using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Client
{
    public interface IGameClient
    {
        List<IActionResult> Update(List<IAction> actions);

        void Init(IEntityContainer entityContainer);
        List<Player> GetPlayers();
        List<ICombatAIEntity> GetEnemies();

        // Sometimes, UDP connection misses and entities are left without calls - this will get them
    }
}
