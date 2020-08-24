using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    public class ContentFonts
    {
        public SpriteFont Arial20;
        public void LoadContent(ContentManager Content)
        {
            Arial20 = Content.Load<SpriteFont>("Arial20");
        }
    }
}
