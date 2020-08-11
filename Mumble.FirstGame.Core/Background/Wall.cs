using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Background
{
    public struct Wall : IBackground
    {
        public bool HasCollision => true;

        public BackgroundSubType SubType { get; private set; }

        public int Scale => 1;

        public Wall(BackgroundSubType subtype)
        {
            SubType = subtype;
        }
    }
}
