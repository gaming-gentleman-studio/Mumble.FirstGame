using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class CursorMetadata : AbstractSpriteMetadata
    {
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Cursor;
        }


        public override Vector2 GetPosition()
        {
            return Mouse.GetState().Position.ToVector2();
        }

    }
}
