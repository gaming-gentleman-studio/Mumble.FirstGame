using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public class SimpleWeaponComponent : IWeaponComponent
    {
        public IVelocityComponent VelocityComponent { get; private set; }

        public IDamageComponent DamageComponent { get; private set; }

        private int _cooldown = 4;
        private int _cooldownLeft = 0;

        public SimpleWeaponComponent(IVelocityComponent velocityComponent, IDamageComponent damageComponent)
        {
            VelocityComponent = velocityComponent;
            DamageComponent = damageComponent;
        }

        public bool AbleToFire()
        {
            return !(_cooldownLeft > 0);
        }

        public List<IProjectileEntity> Fire(float sourceX, float sourceY, Direction direction,IOwnerIdentifier ownerIdentifier)
        {
            _cooldownLeft = _cooldown;
            List<IProjectileEntity> projectiles = new List<IProjectileEntity>();
            SimpleBullet bullet = new SimpleBullet(sourceX, sourceY, DamageComponent.GetRawDamage(), direction, VelocityComponent.Speed, ownerIdentifier);
            projectiles.Add(bullet);
            return projectiles;
        }

        public void ApplyCooldown(int elapsedTicks)
        {
            _cooldownLeft -= elapsedTicks;
        }
    }
}
