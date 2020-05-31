using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Components.Weapon;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
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

        public IOwnerIdentifier OwnerIdentifier { get; private set; }

        private string _name;
        public Player(string name, float x, float y, IOwnerIdentifier owner)
        {
            WeaponComponent = new SimpleWeaponComponent(new VelocityComponent(Direction.None,1),new DamageComponent(3));
            HealthComponent = new HealthComponent(10);
            PositionComponent = new PositionComponent(x, y);
            VelocityComponent = new VelocityComponent(Direction.None,1);
            OwnerIdentifier = owner;
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }
    }
}
