using Mumble.FirstGame.Core.Background;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Size;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle.SceneBoundary
{
    public interface ISceneBoundary
    {
        int Width { get; }
        int Height { get; }

        Dictionary<Direction, int> MaxValues { get; }

        IBackground[,] Backgrounds { get; }
    }
}
