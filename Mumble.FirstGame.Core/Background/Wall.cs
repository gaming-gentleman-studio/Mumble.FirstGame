using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Background
{
    public struct Wall : IBackground
    {
        public bool HasCollision => true;

        public BackgroundSubType SubType { get; private set; }

        public int Scale => 2;

        public IPositionComponent Position { get; private set; }

        public enum WallOrientation
        {
            AtTop,
            AtBottom,
            Other
        }
        public WallOrientation Orientation { get; private set; }

        public Wall(BackgroundSubType subtype, IPositionComponent position, WallOrientation orientation = WallOrientation.Other)
        {
            SubType = subtype;
            Position = position;
            Orientation = orientation;
        }
    }
}
