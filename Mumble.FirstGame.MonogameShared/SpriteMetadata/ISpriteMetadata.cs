using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public interface ISpriteMetadata
    {
        Texture2D GetImage(ContentImages container);
        void AnimateMovement();
        float GetRotation();
        Rectangle GetSpritesheetRectange();
        Vector2 GetPosition();
        Vector2 GetScale();
        Vector2 GetOrigin();
    }
}
