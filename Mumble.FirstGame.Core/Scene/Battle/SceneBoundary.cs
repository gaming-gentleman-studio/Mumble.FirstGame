using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle
{
    public class SceneBoundary
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Dictionary<Direction,int> MaxValues { get; private set; }
        public SceneBoundary(int width, int height)
        {
            Width = width;
            Height = height;
            MaxValues = new Dictionary<Direction, int>() {
                { Direction.Up, 0 },
                { Direction.Down, height },
                { Direction.Left, 0 },
                { Direction.Right, width }
            };
        }
        public bool IsInBounds(IPositionComponent positionComponent)
        {
            return (positionComponent.X <= Width) && (positionComponent.X >= 0) && (positionComponent.Y <= Height) && (positionComponent.Y >= 0);
        }
        public bool IsInBoundsX(IPositionComponent positionComponent)
        {
            return (positionComponent.X <= Width) && (positionComponent.X >= 0);
        }
        public float GetBoundsAdjustedX(IPositionComponent positionComponent)
        {
            if (positionComponent.X > Width)
            {
                return Width;
            }
            else if (positionComponent.X < 0)
            {
                return 0;
            }
            return positionComponent.X;
        }
        public bool IsInBoundsY(IPositionComponent positionComponent)
        {
            return (positionComponent.Y <= Height) && (positionComponent.Y >= 0);
        }
        public float GetBoundsAdjustedY(IPositionComponent positionComponent)
        {
            if (positionComponent.Y > Height)
            {
                return Height;
            }
            else if (positionComponent.Y < 0)
            {
                return 0;
            }
            return positionComponent.Y;
        }
    }
}
