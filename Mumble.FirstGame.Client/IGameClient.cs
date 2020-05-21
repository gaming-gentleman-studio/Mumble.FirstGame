﻿using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Client
{
    public interface IGameClient
    {
        List<IActionResult> Update(List<IAction> actions, TimeSpan elapsed);

        void Init(Player player);
        List<Player> GetPlayers();
        List<ICombatAIEntity> GetEnemies();
    }
}
