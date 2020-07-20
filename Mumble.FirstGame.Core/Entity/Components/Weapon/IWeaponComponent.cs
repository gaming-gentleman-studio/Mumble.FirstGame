using Mumble.FirstGame.Core.Entity.Components.Damage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public interface IWeaponComponent
    {
        IDamageComponent DamageComponent { get; }
        bool AbleToAttack();

        void ApplyCooldown(int elapsedTicks);
    }
}
