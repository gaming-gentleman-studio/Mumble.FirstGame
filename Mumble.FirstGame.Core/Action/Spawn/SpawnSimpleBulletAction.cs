using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Spawn
{
    public class SpawnSimpleBulletAction : ISpawnEntityAction
    {
        public IEntity Entity { get; private set; }

        public List<IActionResult> Results { get; private set; }

        public SpawnSimpleBulletAction(float x, float y, int damage, float speed, Direction direction,IOwnerIdentifier owner)
        {
            Entity = new SimpleBullet(x, y, damage, direction, speed, owner);
        }
        public void CalculateEffect(IEntityContainer container)
        {
            Results = new List<IActionResult>();
            EntitiesCreatedActionResult result = new EntitiesCreatedActionResult(new List<IEntity>() { Entity });
            container.AddEntity(Entity);
            Results.Add(result);
        }
    }
}
