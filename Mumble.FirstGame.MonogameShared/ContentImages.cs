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


        public void LoadContent(ContentManager Content)
        {
            ImgTheDude = Content.Load<Texture2D>("TheDude");
            Bullet = Content.Load<Texture2D>("Bullet");
        }

    }

}
