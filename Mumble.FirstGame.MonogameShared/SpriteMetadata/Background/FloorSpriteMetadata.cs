using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata.Background
{
    public class FloorSpriteMetadata : AbstractSpriteMetadata
    {
        private Vector2 _position;
        private float _scale;
        public FloorSpriteMetadata(Vector2 position, float scale)
        {
            _position = new Vector2(position.X, position.Y);
            _scale = scale;
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Floor;
        }

        public override Vector2 GetPosition()
        {
            return _position;
        }
        public override Vector2 GetScale()
        {
            return new Vector2(_scale, _scale);

        }
        public override float GetLayerDepth()
        {
            return 1f;
        }

    }
}
