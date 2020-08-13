using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Spawn
{
    public class SpawnTurretAction : ISpawnEntityAction
    {
        public IEntity Entity { get; private set; }

        public List<IActionResult> Results { get; private set; }

        public SpawnTurretAction(int health, float x, float y)
        {
            Entity = new Turret(health, x, y);
        }
        public void CalculateEffect(IEntityContainer container)
        {
            container.AddEntity(Entity);
            Results = new List<IActionResult>()
            {
                new EntitiesCreatedActionResult(new List<IEntity>()
                {
                    Entity
                })
            };
        }
    }
}
