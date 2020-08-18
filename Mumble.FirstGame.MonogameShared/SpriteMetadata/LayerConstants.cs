using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public static class LayerConstants
    {
        // ordered furthest back to nearest
        public const float FLOOR = 1f;
        public const float TOPWALL = 0.9f;

        //all the below are the same for now, and set in the abstract class
        // reserving for future use
        public const float PLAYER = 0.2f;
        public const float ENEMY = 0.2f;
        public const float PROJECTILE = 0.2f;

        public const float WALL = 0.1f;
        
        
    }
}

