using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Size;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
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

        public IOwnerIdentifier OwnerIdentifier
        {
            get;
            private set;
        }

        public ISizeComponent SizeComponent => new SmallSizeComponent();

        public SimpleBullet(float x, float y, int damage, Direction direction, float speed, IOwnerIdentifier ownerIdentifier)
        {
            PositionComponent = new PositionComponent(x,y);
            DamageComponent = new DamageComponent(damage);
            VelocityComponent = new VelocityComponent(direction, speed);
            OwnerIdentifier = ownerIdentifier;
        }
        public SimpleBullet(float x, float y, Direction direction, IOwnerIdentifier ownerIdentifier)
        {
            PositionComponent = new PositionComponent(x, y);
            DamageComponent = null;
            VelocityComponent = new VelocityComponent(direction, 0f);
            OwnerIdentifier = ownerIdentifier;
        }
        public string GetName()
        {
            return "Simple Bullet";
        }
    }
}
