using Mumble.FirstGame.Core.Entity.Components.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Background
{
    public struct Floor : IBackground
    {
        public bool HasCollision => false;


        public int Scale => 1;

        public IPositionComponent Position { get; private set; }

        public Floor(IPositionComponent position)
        {
            Position = position;
        }
    }
}
