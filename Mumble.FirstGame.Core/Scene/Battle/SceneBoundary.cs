using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle
{
    public class SceneBoundary
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Dictionary<MoveAction.DirectionValues,int> MaxValues { get; private set; }
        public SceneBoundary(int width, int height)
        {
            Width = width;
            Height = height;
            MaxValues = new Dictionary<MoveAction.DirectionValues, int>() {
                { MoveAction.DirectionValues.Up,height },
                { MoveAction.DirectionValues.Down, 0 },
                { MoveAction.DirectionValues.Left, 0 },
                { MoveAction.DirectionValues.Right, width }
            };
        }
        public bool IsInBounds(IPositionComponent positionComponent)
        {
            return (positionComponent.X <= Width) && (positionComponent.X >= 0) && (positionComponent.Y <= Height) && (positionComponent.Y >= 0);
        }
    }
}
