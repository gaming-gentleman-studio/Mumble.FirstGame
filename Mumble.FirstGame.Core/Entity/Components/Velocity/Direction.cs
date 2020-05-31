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
        public readonly float Radians;
        public Direction(float x, float y)
        {
            X = 0f;
            Y = 0f;
            Radians = 0f;
            Tuple<float, float> normalized = Normalize(x, y);
            X = normalized.Item1;
            Y = normalized.Item2;
            Radians = (float)Math.Atan2(Y, X);
        }
        private Tuple<float,float> Normalize(float x, float y)
        {
            float len = Math.Abs(x *x + y * y);
            float normalizedX = x / len;
            float normalizedY = y / len;
            return new Tuple<float, float>(normalizedX, normalizedY);
        }
        public Direction(float radians)
        {
            X = (float)Math.Cos(radians);
            Y = (float)Math.Sin(radians);
            Radians = radians;
        }
        public static Direction Up => new Direction(0, -1);
        public static Direction Down => new Direction(0, 1);
        public static Direction Left => new Direction(-1, 0);
        public static Direction Right => new Direction(1, 0);
        public static Direction None => new Direction(0, 0);
    }
}
