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

        public MoveAction(DirectionValues direction)
        {
            Direction = direction;
            Result = new ActionResult(TagId.move, Direction);
        }
        public bool HasResult()
        {
            return true;
        }
    }
}
