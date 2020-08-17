using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Background
{
    public interface IBackground
    {
        bool HasCollision { get; }
        BackgroundSubType SubType { get; }

        IPositionComponent Position { get; }

        int Scale { get; }
    }
}
