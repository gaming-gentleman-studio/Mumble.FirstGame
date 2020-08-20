using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.DisplayHandlers
{
    public interface IDisplayHandler
    {
        SpriteBatch Draw(Vector2 mousePosition);
    }
}
