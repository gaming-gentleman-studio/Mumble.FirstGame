using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Background;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle.SceneBoundary
{
    public class BattleSceneBoundary : ISceneBoundary
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Dictionary<Direction,int> MaxValues { get; private set; }

        public IBackground[,] Backgrounds { get; private set; }


        public BattleSceneBoundary(int width, int height, IBackground[,] backgrounds)
        {
            Width = width;
            Height = height;
            MaxValues = new Dictionary<Direction, int>() {
                { Direction.Up, 0 },
                { Direction.Down, height },
                { Direction.Left, 0 },
                { Direction.Right, width }
            };
            Backgrounds = backgrounds;
        }
        private bool HasBackgroundCollision(IPositionComponent positionComponent)
        {
            int x = (int)Math.Round(positionComponent.X, 0);
            int y = (int)Math.Round(positionComponent.Y, 0);
            if (Backgrounds[x, y].HasCollision)
            {
                return true;
            }
            return false;
        }
        public bool IsInBounds(IPositionComponent positionComponent)
        {
            if (HasBackgroundCollision(positionComponent))
            {
                return false;
            }
            return (positionComponent.X <= Width) && (positionComponent.X >= 0) && (positionComponent.Y <= Height) && (positionComponent.Y >= 0);
        }
        //TODO - figure out a more intelligent way to bounce an entity
        public bool IsInBoundsX(IPositionComponent positionComponent)
        {
            if (HasBackgroundCollision(positionComponent))
            {
                return false;
            }
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
            if (HasBackgroundCollision(positionComponent))
            {
                return false;
            }
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
