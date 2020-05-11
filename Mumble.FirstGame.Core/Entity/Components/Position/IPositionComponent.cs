using Mumble.FirstGame.Core.Action.Movement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Position
{
    public interface IPositionComponent : IEntityComponent
    {
        int X { get;  }
        int Y { get; }

        IPositionComponent GetNewCoords(MoveAction.DirectionValues direction);
        void Move(IPositionComponent destinationPosition);
    }
}
