using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class CursorMetadata : ISpriteMetadata
    {
        public void Animate()
        {
            return;
        }

        public Texture2D GetImage(ContentImages container)
        {
            return container.Cursor;
        }

        public Vector2 GetOrigin()
        {
            return Vector2.Zero;
        }

        public Vector2 GetPosition()
        {
            return Mouse.GetState().Position.ToVector2();
        }

        public float GetRotation()
        {
            return 0;
        }

        public Vector2 GetScale()
        {
            return new Vector2(1, 1);
        }

        public Rectangle GetSpritesheetRectange()
        {
            return new Rectangle(0, 0, 16, 16);
        }
    }
}
