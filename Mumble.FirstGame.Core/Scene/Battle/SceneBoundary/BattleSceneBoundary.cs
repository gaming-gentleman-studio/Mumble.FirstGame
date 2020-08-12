using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Background;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Size;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
    }
}
