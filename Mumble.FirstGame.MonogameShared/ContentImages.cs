using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Mumble.FirstGame.MonogameShared
{
    class ContentImages
    {
        public Texture2D ImgTheDude { get; set; }

        ImgTheDude = Content.Load<Texture2D>("The Dude");
    }
}
