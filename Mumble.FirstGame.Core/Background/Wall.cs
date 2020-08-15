using Mumble.FirstGame.Core.Entity.Components.Position;
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

        public Wall(BackgroundSubType subtype, IPositionComponent position)
        {
            SubType = subtype;
            Position = position;
        }
    }
}
