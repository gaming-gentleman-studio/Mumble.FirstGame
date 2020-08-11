using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Background
{
    public struct Floor : IBackground
    {
        public bool HasCollision => false;

        public BackgroundSubType SubType { get; private set; }

        public int Scale => 1;

        public Floor(BackgroundSubType subtype)
        {
            SubType = subtype;
        }
    }
}
