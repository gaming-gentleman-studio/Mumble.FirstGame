using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Scene.Battle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Movement
{
    public class MoveAction : IMoveAction
    {
        public enum DirectionValues
        {
            Left,
            Right,
            Up,
            Down
        }
        public DirectionValues Direction { get; private set; }
        public ActionResult Result { get; private set; }
        private IMoveableEntity _entity;

        public MoveAction(IMoveableEntity entity, DirectionValues direction)
        {
            _entity = entity;
            Direction = direction;
            
        }
        public bool HasResult()
        {
            return true;
        }

        public void CalculateEffect(SceneBoundary boundary)
        {
            IPositionComponent newPosition = _entity.PositionComponent.GetNewCoords(Direction);
            if (boundary.IsInBounds(newPosition))
            {
                _entity.PositionComponent.Move(newPosition);
                Result = new ActionResult(TagId.move, _entity.GetName(), newPosition.X, newPosition.Y);
            }
            else
            {
                if ((Direction == DirectionValues.Up) || (Direction == DirectionValues.Down))
                {
                    newPosition = new PositionComponent(newPosition.X, boundary.MaxValues[Direction]);
                }
                else if ((Direction == DirectionValues.Left) || (Direction == DirectionValues.Right))
                {
                    newPosition = new PositionComponent(boundary.MaxValues[Direction], newPosition.Y);
                }
                _entity.PositionComponent.Move(newPosition);
                Result = new ActionResult(TagId.move_out_of_bounds, _entity.GetName(), Direction, _entity.PositionComponent.X, _entity.PositionComponent.Y);
            }
            
        }
    }
}
