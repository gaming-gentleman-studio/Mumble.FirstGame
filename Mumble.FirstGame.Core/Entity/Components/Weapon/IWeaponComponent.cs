using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public interface IWeaponComponent
    {
        IVelocityComponent VelocityComponent { get; }
        IDamageComponent DamageComponent { get; }
    }
}
