using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public class SimpleWeaponComponent : IRangedWeaponComponent
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

        public bool AbleToAttack()
        {
            return !(_cooldownLeft > 0);
        }

        public IAction Attack(float sourceX, float sourceY, Direction direction,IOwnerIdentifier ownerIdentifier)
        {
            _cooldownLeft = _cooldown;
            SpawnSimpleBulletAction spawn = new SpawnSimpleBulletAction(sourceX,sourceY, DamageComponent.GetRawDamage(), VelocityComponent.Speed, direction,ownerIdentifier);
            return spawn;
        }

        public void ApplyCooldown(int elapsedTicks)
        {
            _cooldownLeft -= elapsedTicks;
        }
    }
}
