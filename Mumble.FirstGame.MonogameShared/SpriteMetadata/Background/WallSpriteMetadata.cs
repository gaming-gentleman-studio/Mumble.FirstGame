using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Background;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata.Background
{
    public class WallSpriteMetadata : AbstractSpriteMetadata
    {
        private Vector2 _position;
        public WallSpriteMetadata(Vector2 position)
        {
            _position = new Vector2(position.X, position.Y);
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Wall;
        }

        public override Vector2 GetPosition()
        {
            return _position;
        }
    }
}
