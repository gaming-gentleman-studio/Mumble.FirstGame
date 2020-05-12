using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.TagArguments
{
    public struct MoveArguments : ITagArguments
    {
        public readonly string SubjectName;
        public readonly int XPos;
        public readonly int YPos;
        public MoveArguments(string subjectName, int x, int y)
        {
            SubjectName = subjectName;
            XPos = x;
            YPos = y;
        }
    }
}
