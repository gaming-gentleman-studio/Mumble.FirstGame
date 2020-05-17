using Mumble.FirstGame.Core.Entity.Components;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity
{
    public interface ICombatEntity : IMoveableEntity
    {
        IHealthComponent HealthComponent
        {
            get;
        }
        IDamageComponent DamageComponent
        {
            get;
        }

    }
}
