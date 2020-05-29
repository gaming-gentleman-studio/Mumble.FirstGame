using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Components.Weapon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Player
{
    public class Player : IPlayerControlledEntity
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
        private string _name;
        public Player(string name, int damage, int health)
        {
            WeaponComponent = new SimpleWeaponComponent(new VelocityComponent(Direction.None,1),new DamageComponent(damage));
            HealthComponent = new HealthComponent(health);
            PositionComponent = new PositionComponent(0, 0);
            VelocityComponent = new VelocityComponent(Direction.None,1);
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
