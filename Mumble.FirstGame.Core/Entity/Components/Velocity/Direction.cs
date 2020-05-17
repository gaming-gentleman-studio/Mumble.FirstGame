using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Velocity
{
    public struct Direction
    {
        public readonly float X;
        public readonly float Y;

        public Direction(float x, float y)
        {
            if ((x > 1) || (x < -1))
            {
                throw new Exception("Invalid X argument to direction - must be between -1 and 1");
            }
            if ((y>1) || (y < -1))
            {
                throw new Exception("Invalid Y argument to direction - must be between -1 and 1");

            }
            X = x;
            Y = y;
        }
        public static Direction Up => new Direction(0, -1);
        public static Direction Down => new Direction(0, 1);
        public static Direction Left => new Direction(-1, 0);
        public static Direction Right => new Direction(1, 0);
        public static Direction None => new Direction(0, 0);
    }
}
