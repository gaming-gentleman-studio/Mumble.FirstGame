using Mumble.FirstGame.Core.Entity;
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
        public readonly bool OutOfBounds; 
        public MoveActionResult(IEntity entity, float x, float y, bool outOfBounds = false)
        {
            Entity = entity;
            XPos = x;
            YPos = y;
            OutOfBounds = outOfBounds;
        }
    }
}
