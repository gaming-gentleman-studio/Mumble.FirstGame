using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Fire
{
    public class FireWeaponAction : IFireWeaponAction
    {
        private Direction _direction;
        private ICombatEntity _sourceEntity;

        public IActionResult Result
        {
            get;
            private set;
        }

        
        public FireWeaponAction(ICombatEntity sourceEntity, Direction direction)
        {
            _sourceEntity = sourceEntity;
            _direction = direction;
        }
        public List<IProjectileEntity> CalculateEffect()
        {
            List<IProjectileEntity> projectiles = new List<IProjectileEntity>();
            SimpleBullet bullet = new SimpleBullet(new PositionComponent(_sourceEntity.PositionComponent.X,_sourceEntity.PositionComponent.Y), new DamageComponent(_sourceEntity.DamageComponent.GetRawDamage()), _direction);
            projectiles.Add(bullet);
            Result = new EntitiesCreatedActionResult(projectiles.ToList<IEntity>());
            return projectiles;
        }
        public bool HasResult()
        {
            throw new NotImplementedException();
        }
    }
}
