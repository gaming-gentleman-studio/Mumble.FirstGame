using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    class ContentImages
    {
        public Texture2D ImgTheDude { get; set; }


        public void LoadContent(ContentManager Content)
        {
            ImgTheDude = Content.Load<Texture2D>("TheDude");
        }

    }

}
