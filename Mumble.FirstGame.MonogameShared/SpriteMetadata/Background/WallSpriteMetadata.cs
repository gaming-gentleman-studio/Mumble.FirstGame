using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Background;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;
using static Mumble.FirstGame.Core.Background.Wall;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata.Background
{
    public class WallSpriteMetadata : AbstractSpriteMetadata
    {
        private Wall _wall;
        public WallSpriteMetadata(Wall wall)
        {
            _wall = wall;
        }
        public override Rectangle GetSpritesheetRectange(Vector2 mousePosition)
        {
            if (_wall.Orientation == WallOrientation.AtTop)
            {
                return new Rectangle(0, 0, 16, 32);
            }
            return new Rectangle(0, 0, 16, 16);
            
        }
        public override Texture2D GetImage(ContentImages container)
        {
            if (_wall.Style.DoodadSet == WallDoodadSet.FracturedTop)
            {
                return container.DungeonWallFracturedTop;
            }
            else
            {
                return container.DungeonWall;
            }
            
        }

        public override Vector2 GetPosition()
        {
            return new Vector2(_wall.Position.X, _wall.Position.Y);

        }
        public override Vector2 GetScale()
        {
            return new Vector2(_wall.Scale, _wall.Scale);

        }
        public override float GetLayerDepth()
        {
            if (_wall.Orientation == WallOrientation.AtTop)
            {
                return LayerConstants.TOPWALL;
            }
            else
            {
                return LayerConstants.WALL;
            }
            
        }
        public override Vector2 GetOrigin()
        {
            return new Vector2(8, 8);
        }

    }
}
