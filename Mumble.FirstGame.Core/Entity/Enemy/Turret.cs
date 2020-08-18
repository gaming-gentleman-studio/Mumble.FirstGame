using Mumble.FirstGame.Core.Entity.Components.AI;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Size;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Components.Weapon;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Enemy
{
    public class Turret : ICombatAIEntity
    {
        public ICombatAIComponent CombatAIComponent { get; private set; }

        public IHealthComponent HealthComponent { get; private set; }

        public IWeaponComponent WeaponComponent { get; private set; }

        public IVelocityComponent VelocityComponent { get; private set; }

        public IPositionComponent PositionComponent { get; private set; }

        public IOwnerIdentifier OwnerIdentifier { get; private set; }

        public ISizeComponent SizeComponent { get; private set; }

        public string GetName()
        {
            return "Turret";
        }
        public Turret(int health,float x, float y)
        {
            CombatAIComponent = new SimpleAttackPlayerAIComponent();
            HealthComponent = new HealthComponent(health);
            WeaponComponent = new FireballShooterWeaponComponent(new VelocityComponent(Direction.None, 1f), new DamageComponent(1));
            VelocityComponent = new VelocityComponent(Direction.None, 0f);
            PositionComponent = new PositionComponent(x, y);
            OwnerIdentifier = IntOwnerIdentifier.NonPlayerOwned;
            SizeComponent = new MediumSizeComponent();
        }
    }
}
