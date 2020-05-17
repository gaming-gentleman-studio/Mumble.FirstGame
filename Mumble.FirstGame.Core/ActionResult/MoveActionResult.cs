using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class MoveActionResult : IActionResult
    {
        public readonly string SubjectName;
        public readonly float XPos;
        public readonly float YPos;
        public readonly bool OutOfBounds; 
        public MoveActionResult(string subjectName, float x, float y, bool outOfBounds = false)
        {
            SubjectName = subjectName;
            XPos = x;
            YPos = y;
            OutOfBounds = outOfBounds;
        }
    }
}
