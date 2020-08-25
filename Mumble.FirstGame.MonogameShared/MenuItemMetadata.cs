using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Scene.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    public class MenuItemMetadata
    {
        public string Text;
        public MenuOption Option;
        public SpriteFont Font;
        public Vector2 Position;
        public Vector2 Size;
        public Vector2 Origin;
        public Rectangle Bounds;
        public MenuItemMetadata(MenuOption option,Vector2 position, SpriteFont font)
        {
            Option = option;
            Text = option.ToString();
            Position = position;
            Font = font;
            Size = font.MeasureString(Text);
            Origin = Size * 0.5f;
            Vector2 topLeft = new Vector2(position.X - (Size.X * 0.5f), position.Y - (Size.Y * 0.5f));
            Bounds = new Rectangle(topLeft.ToPoint(), Size.ToPoint());
        }
    }
}
