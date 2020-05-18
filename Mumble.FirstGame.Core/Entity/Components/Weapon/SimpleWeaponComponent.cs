using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public class SimpleWeaponComponent : IWeaponComponent
    {
        public IVelocityComponent VelocityComponent { get; private set; }

        public IDamageComponent DamageComponent { get; private set; }

        public SimpleWeaponComponent(IVelocityComponent velocityComponent, IDamageComponent damageComponent)
        {
            VelocityComponent = velocityComponent;
            DamageComponent = damageComponent;
        }
    }
}
