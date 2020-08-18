using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public class FireballShooterWeaponComponent : IRangedWeaponComponent
    {
        public IVelocityComponent VelocityComponent { get; private set; }

        public IDamageComponent DamageComponent { get; private set; }

        private int _cooldown = 4;
        private int _cooldownLeft = 0;

        public FireballShooterWeaponComponent(IVelocityComponent velocityComponent, IDamageComponent damageComponent)
        {
            VelocityComponent = velocityComponent;
            DamageComponent = damageComponent;
        }

        public bool AbleToAttack()
        {
            return !(_cooldownLeft > 0);
        }

        public IAction Attack(float sourceX, float sourceY, Direction direction, IOwnerIdentifier ownerIdentifier)
        {
            _cooldownLeft = _cooldown;
            SpawnEntityAction spawn = new SpawnEntityAction(new Fireball(sourceX, sourceY, DamageComponent.GetRawDamage(), direction, VelocityComponent.Speed, ownerIdentifier));
            return spawn;
        }

        public void ApplyCooldown(int elapsedTicks)
        {
            _cooldownLeft -= elapsedTicks;
        }
    }
}
