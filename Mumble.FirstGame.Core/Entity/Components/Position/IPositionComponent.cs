using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Position
{
    public interface IPositionComponent : IEntityComponent
    {
        float X { get;  }
        float Y { get; }

        Direction Facing { get; }
        IPositionComponent GetNewCoords(IVelocityComponent velocity);
        void Move(IPositionComponent destinationPosition);

        void ChangeFacing(Direction direction);

    }
}
