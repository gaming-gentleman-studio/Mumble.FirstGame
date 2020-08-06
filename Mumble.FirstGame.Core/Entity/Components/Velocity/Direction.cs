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
        private const float MAX_RADIANS = 2 * (float)Math.PI;
        public Direction(float x, float y)
        {
            X = 0f;
            Y = 0f;
            Radians = 0f;
            Tuple<float, float> normalized = Normalize(x, y);
            X = normalized.Item1;
            Y = normalized.Item2;
            Radians = (float)Math.Atan2(Y, X);
            //wtf why is this needed help me plz
            if (Radians == -(float)Math.PI)
            {
                Radians = (float)Math.PI;
            }

        }
        
        public Direction(float radians) : this((float)Math.Cos(radians), (float)Math.Sin(radians))
        {
        }
        private Tuple<float, float> Normalize(float x, float y)
        {
            x = (float) Math.Round((decimal)x, 3, MidpointRounding.AwayFromZero);
            y = (float)Math.Round((decimal)y, 3, MidpointRounding.AwayFromZero);
            float len = (float) Math.Sqrt(Math.Abs(x * x + y * y));
            float normalizedX = x / len;
            float normalizedY = y / len;
            return new Tuple<float, float>(normalizedX, normalizedY);
        }
        public override int GetHashCode()
        {
            return Radians.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Direction))
            {
                return false;
            }
            Direction other = (Direction)obj;
            return Radians.Equals(other.Radians);
        }
        public static Direction Up => new Direction(0, -1);
        public static Direction Down => new Direction(0, 1);
        public static Direction Left => new Direction(-1f, 0f);
        public static Direction Right => new Direction(1, 0);
        public static Direction None => new Direction(0, 0);

        public static Direction GetRandom90Direction()
        {
            Random rand = new Random();
            int randInt = rand.Next(0, 3);
            switch (randInt)
            {
                case 0:
                    return Direction.Up;
                case 1:
                    return Direction.Down;
                case 2:
                    return Direction.Left;
                case 3:
                    return Direction.Right;
                default:
                    return Direction.Up;
            }


        }
        public static Direction ToNearest90Angle(Direction original) => ToNearestAngle(original, 4);
        private static Direction ToNearestAngle(Direction original, int numOfPieces)
        {
            float pieceSize = MAX_RADIANS / numOfPieces;
            float half = pieceSize / 2;
            int part = (int)Math.Floor(original.Radians / pieceSize);
            float diff = original.Radians - (pieceSize * part);
            if (diff > half)
            {
                return new Direction(pieceSize * (part + 1));
            }
            else
            {
                return new Direction(pieceSize * part);
            }
        }
    }
}
