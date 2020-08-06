using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class MoveActionResult : IActionResult
    {
        public readonly IEntity Entity;
        public readonly float XPos;
        public readonly float YPos;
        public readonly float OldXPos;
        public readonly float OldYPos;
        public readonly Direction Direction;
        public readonly bool OutOfBounds; 
        public MoveActionResult(IEntity entity, float x, float y, float oldX, float oldY,bool outOfBounds = false)
        {
            Entity = entity;
            XPos = x;
            YPos = y;
            OldXPos = oldX;
            OldYPos = oldY;
            OutOfBounds = outOfBounds;
            Direction = new Direction(x - oldX, y - oldY);   
        }
    }
}
