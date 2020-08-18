using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    public class ContentImages
    {

        public Texture2D ImgTheDude { get; private set; }
        public Texture2D Bullet { get; private set; }
        public Texture2D Fireball { get; private set; }
        public Texture2D Cursor { get; private set; }

        public Texture2D Slime { get; private set; }

        public Texture2D DungeonWall { get; private set; }

        public Texture2D DungeonWallFracturedTop { get; private set; }

        public Texture2D Turret { get; private set; }

        public Texture2D Floor { get; private set; }

        public void LoadContent(ContentManager Content, GraphicsDevice graphicsDevice)
        {
            ImgTheDude = Content.Load<Texture2D>("Sprites/TheDude");
            Bullet = Content.Load<Texture2D>("Projectiles/Bullet");
            Cursor = Content.Load<Texture2D>("UI/Cursor");
            Slime = Content.Load<Texture2D>("Sprites/Slime");
            DungeonWall = Content.Load<Texture2D>("Background/DungeonWall");
            DungeonWallFracturedTop = Content.Load<Texture2D>("Background/DungeonWall-FracturedTop");
            Turret = Content.Load<Texture2D>("Sprites/Turret");
            Floor = Content.Load<Texture2D>("Background/Floor1");
            Fireball = Content.Load<Texture2D>("Projectiles/Fireball");
        }

    }

}
