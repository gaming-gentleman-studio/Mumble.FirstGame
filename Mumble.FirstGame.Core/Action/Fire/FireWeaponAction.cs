using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Fire
{
    public class FireWeaponAction : IFireWeaponAction
    {
        public Direction Direction { get; private set; }
        public ICombatEntity Entity { get; private set; }

        public List<IActionResult> Results
        {
            get;
            private set;
        }

        
        public FireWeaponAction(ICombatEntity sourceEntity, Direction direction)
        {
            Entity = sourceEntity;
            Direction = direction;
            Results = new List<IActionResult>();
        }
        public List<IProjectileEntity> CalculateEffect(int elapsedTicks)
        {
            List<IProjectileEntity> projectiles = new List<IProjectileEntity>();
            if (Entity.WeaponComponent.AbleToFire(elapsedTicks))
            {
                projectiles = Entity.WeaponComponent.Fire(Entity.PositionComponent.X, Entity.PositionComponent.Y, Direction);
            }
            Results.Add(new EntitiesCreatedActionResult(projectiles.ToList<IEntity>()));
            return projectiles;
        }
        public bool HasResult()
        {
            throw new NotImplementedException();
        }
    }
}
