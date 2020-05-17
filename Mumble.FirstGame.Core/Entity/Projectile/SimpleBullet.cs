using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Projectile
{
    public class SimpleBullet : IProjectileEntity
    {
        public IDamageComponent DamageComponent
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

        public SimpleBullet(IPositionComponent sourceEntityPosition, IDamageComponent sourceEntityDamage, Direction direction)
        {
            PositionComponent = sourceEntityPosition;
            DamageComponent = sourceEntityDamage;
            VelocityComponent = new VelocityComponent(direction, 1);
        }
        public string GetName()
        {
            return "Simple Bullet";
        }
    }
}
