using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
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

        private TimeSpan _cooldown = TimeSpan.FromMilliseconds(100);
        private TimeSpan _cooldownLeft = TimeSpan.FromSeconds(0);

        public SimpleWeaponComponent(IVelocityComponent velocityComponent, IDamageComponent damageComponent)
        {
            VelocityComponent = velocityComponent;
            DamageComponent = damageComponent;
        }

        public bool AbleToFire(TimeSpan elapsed)
        {
            _cooldownLeft -= elapsed;
            return !(_cooldownLeft > TimeSpan.FromSeconds(0));
        }

        public List<IProjectileEntity> Fire(float sourceX, float sourceY, Direction direction)
        {
            _cooldownLeft = _cooldown;
            List<IProjectileEntity> projectiles = new List<IProjectileEntity>();
            SimpleBullet bullet = new SimpleBullet(sourceX, sourceY, DamageComponent.GetRawDamage(), direction, VelocityComponent.Speed);
            projectiles.Add(bullet);
            return projectiles;
        }
    }
}
