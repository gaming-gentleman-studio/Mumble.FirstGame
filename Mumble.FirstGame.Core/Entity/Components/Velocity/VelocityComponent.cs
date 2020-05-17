using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Velocity
{
    public class VelocityComponent : IVelocityComponent
    {
        //TODO - should we have a separate component for entity speed?
        public float Speed
        {
            get;
            private set;
        }

        public Direction Direction
        {
            get;
            private set;
        }

        public VelocityComponent(Direction direction, float speed)
        {
            Direction = direction;
            Speed = speed;
        }
    }
}
