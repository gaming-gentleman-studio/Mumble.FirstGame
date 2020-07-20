using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public class SimpleMeleeWeaponComponent : IRangedWeaponComponent
    {
        public IVelocityComponent VelocityComponent { get; private set; }

        public IDamageComponent DamageComponent { get; private set; }

        private int _cooldown = 4;
        private int _cooldownLeft = 0;
        public bool AbleToAttack()
        {
            return !(_cooldownLeft > 0);
        }

        public void ApplyCooldown(int elapsedTicks)
        {
            _cooldownLeft -= elapsedTicks;
        }

        public IAction Attack(float sourceX, float sourceY, Direction direction, IOwnerIdentifier ownerIdentifier)
        {
            return null;
        }
    }
}
