using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Position
{
    public class PositionComponent : IPositionComponent
    {

        public float X
        {
            get;
            private set;
        }
        public float Y
        {
            get;
            private set;
        }
        public PositionComponent(float x, float y)
        {
            X = x;
            Y = y;
        }
        public IPositionComponent GetNewCoords(IVelocityComponent velocity)
        {
            return new PositionComponent(X + (velocity.Direction.X * velocity.Speed), Y + (velocity.Direction.Y * velocity.Speed));
        }
        public void Move(IPositionComponent destinationPosition)
        {
            X = destinationPosition.X;
            Y = destinationPosition.Y;
        }
    }
}
