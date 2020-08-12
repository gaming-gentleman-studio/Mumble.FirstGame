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
        private float _scale;
        public WallSpriteMetadata(Vector2 position, float scale)
        {
            _position = new Vector2(position.X, position.Y);
            _scale = scale;
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Wall;
        }

        public override Vector2 GetPosition()
        {
            return _position;
        }
        public override Vector2 GetScale()
        {
            return new Vector2(_scale, _scale);

        }

    }
}
