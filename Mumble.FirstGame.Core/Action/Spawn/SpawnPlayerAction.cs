using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Spawn
{
    public class SpawnPlayerAction : ISpawnEntityAction
    {
        public IEntity Entity { get; private set; }

        public List<IActionResult> Results { get; private set; }

        public SpawnPlayerAction(string name, int damage, int health)
        {
            Entity = new Player(name, damage, health);
            Results = new List<IActionResult>();
        }
        public IEntity CalculateEffect()
        {
            EntitiesCreatedActionResult result = new EntitiesCreatedActionResult(new List<IEntity>() { Entity });
            Results.Add(result);
            return Entity;
        }
    }
}
