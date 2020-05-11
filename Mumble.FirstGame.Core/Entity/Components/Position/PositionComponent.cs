using Mumble.FirstGame.Core.Action.Movement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Position
{
    public class PositionComponent : IPositionComponent
    {
        private static Dictionary<MoveAction.DirectionValues, PositionComponent> DirectionMap = new Dictionary<MoveAction.DirectionValues, PositionComponent>()
        {
            { MoveAction.DirectionValues.Up, new PositionComponent(0,1) },
            { MoveAction.DirectionValues.Down, new PositionComponent(0,-1) },
            { MoveAction.DirectionValues.Left, new PositionComponent(-1,0) },
            { MoveAction.DirectionValues.Right, new PositionComponent(1,0) }
        };
        public int X
        {
            get;
            private set;
        }
        public int Y
        {
            get;
            private set;
        }
        public PositionComponent(int x, int y)
        {
            X = x;
            Y = y;
        }
        public IPositionComponent GetNewCoords(MoveAction.DirectionValues direction)
        {
            PositionComponent distanceMoved = DirectionMap[direction];
            return new PositionComponent(X + distanceMoved.X, Y + distanceMoved.Y);
        }
        public void Move(IPositionComponent destinationPosition)
        {
            X = destinationPosition.X;
            Y = destinationPosition.Y;
        }
    }
}
