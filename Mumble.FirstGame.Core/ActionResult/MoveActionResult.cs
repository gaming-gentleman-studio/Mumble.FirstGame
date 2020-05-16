using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class MoveActionResult : IActionResult
    {
        public readonly string SubjectName;
        public readonly int XPos;
        public readonly int YPos;
        public readonly bool OutOfBounds; 
        public MoveActionResult(string subjectName, int x, int y, bool outOfBounds = false)
        {
            SubjectName = subjectName;
            XPos = x;
            YPos = y;
            OutOfBounds = outOfBounds;
        }
    }
}
