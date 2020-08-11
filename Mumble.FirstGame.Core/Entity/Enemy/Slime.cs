using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Entity.Components.AI;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Components.Weapon;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Enemy
{
    public class Slime : ICombatAIEntity
    {
        public IWeaponComponent WeaponComponent
        {
            get;
            private set;
        }
        public IHealthComponent HealthComponent
        {
            get;
            private set;
        }
        public ICombatAIComponent CombatAIComponent
        {
            get;
            private set;
        }

        public IPositionComponent PositionComponent
        {
            get;
            private set;
        }

        public IVelocityComponent VelocityComponent
        {
            get;
            private set;
        }

        public IOwnerIdentifier OwnerIdentifier
        {
            get;
            private set;
        }

        public int Scale => 2;

        public Slime(int damage, int health, float x, float y)
        {
            WeaponComponent = new SimpleMeleeWeaponComponent(new DamageComponent(damage));
            HealthComponent = new HealthComponent(health);
            CombatAIComponent = new SimpleFollowPlayerAIComponent();
            PositionComponent = new PositionComponent(x, y);
            VelocityComponent = new VelocityComponent(Direction.None, 0.5f);
            OwnerIdentifier = IntOwnerIdentifier.NonPlayerOwned;

        }

        public string GetName()
        {
            return "Slime";
        }
    }
}
