using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    public class MenuItemMetadata
    {
        public string Text;
        public SpriteFont Font;
        public Vector2 Position;
        public Vector2 Size;
        public Vector2 Origin;
        public Rectangle Bounds;
        public MenuItemMetadata(string text, Vector2 position, SpriteFont font)
        {
            Text = text;
            Position = position;
            Font = font;
            Size = font.MeasureString(text);
            Origin = Size * 0.5f;
            Bounds = new Rectangle(Origin.ToPoint(), Size.ToPoint());
        }
    }
}
