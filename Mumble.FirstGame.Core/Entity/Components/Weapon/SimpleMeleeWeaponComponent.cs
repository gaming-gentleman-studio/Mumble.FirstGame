using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public class SimpleMeleeWeaponComponent : IMeleeWeaponComponent
    {

        private int _cooldown = 50;
        private int _cooldownLeft = 0;

        public IDamageComponent DamageComponent { get; private set; }

        public SimpleMeleeWeaponComponent(IDamageComponent damageComponent)
        {
            DamageComponent = damageComponent;
        }

        

        public bool AbleToAttack()
        {
            
            return !(_cooldownLeft > 0);
            
        }

        public void ApplyCooldown(int elapsedTicks)
        {
            _cooldownLeft -= elapsedTicks;
        }

        public IActionResult Attack(ICombatEntity target)
        {
            _cooldownLeft = _cooldown;
            target.HealthComponent.Hit(DamageComponent.GetRawDamage());
            if (!target.HealthComponent.IsAlive())
            {
                return new EntityDestroyedActionResult(target);
            }
            else
            {
                return new DamageActionResult(target, DamageComponent.GetRawDamage());
            }
        }
    }
}
