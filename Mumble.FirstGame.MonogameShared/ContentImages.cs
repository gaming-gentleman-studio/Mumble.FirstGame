using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    class ContentImages
    {
        public Dictionary<Direction, Rectangle> SpritesheetPosByDirection = new Dictionary<Direction, Rectangle>()
        {
            {Direction.Down , new Rectangle(0,0,16,16) },
            {Direction.Up, new Rectangle(16,0,16,16) },
            {Direction.Right, new Rectangle(32,0,16,16) },
            {Direction.Left, new Rectangle(48,0,16,16) }
        };
        public Texture2D ImgTheDude { get; private set; }
        public Texture2D Bullet { get; private set; }


        public void LoadContent(ContentManager Content)
        {
            ImgTheDude = Content.Load<Texture2D>("TheDude");
            Bullet = Content.Load<Texture2D>("Bullet");
        }

    }

}
