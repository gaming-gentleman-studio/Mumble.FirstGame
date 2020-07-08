using Microsoft.Xna.Framework;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Utils
{
    public static class Vector2Extension
    {
        public static Direction ToDirection(this Vector2 vec)
        {
            return new Direction(vec.X, vec.Y);
        }
        public static Direction ToRelativeDirection(this Vector2 vec, Vector2 source)
        {
            Vector2 directionVector = new Vector2(vec.X - source.X, vec.Y - source.Y);
            directionVector.Normalize();
            return directionVector.ToDirection();
        }
        public static Direction ToRelativeDirection(this Vector2 vec, Direction source)
        {
            Vector2 directionVector = new Vector2(vec.X - source.X, vec.Y - source.Y);
            directionVector.Normalize();
            return directionVector.ToDirection();
        }
    }
}
