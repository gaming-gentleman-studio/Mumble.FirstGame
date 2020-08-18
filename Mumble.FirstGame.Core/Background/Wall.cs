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


        public int Scale => 2;

        public IPositionComponent Position { get; private set; }

        public enum WallOrientation
        {
            AtTop,
            AtBottom,
            Other
        }
        public WallOrientation Orientation { get; private set; }

        public WallStyle Style { get; private set; }
        public Wall(IPositionComponent position,WallStyle style, WallOrientation orientation = WallOrientation.Other)
        {
            Position = position;
            Orientation = orientation;
            Style = style;
        }
    }
}
