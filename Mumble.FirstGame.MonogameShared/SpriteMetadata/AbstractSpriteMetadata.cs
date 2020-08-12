using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public abstract class AbstractSpriteMetadata
    {
        public abstract Texture2D GetImage(ContentImages container);
        public abstract Vector2 GetPosition();
        public virtual void AnimateMovement(MoveActionResult result)
        {
            return;
        }
        public virtual void AnimateDamage()
        {
            return;
        }
        public virtual void AnimateAttack()
        {
            return;
        }
        public virtual float GetRotation()
        {
            return 0;
        }
        public virtual Color GetColor()
        {
            return Color.DarkGray;
        }
        public virtual Rectangle GetSpritesheetRectange()
        {
            return new Rectangle(0, 0, 16, 16);
        }
        
        public virtual Vector2 GetScale()
        {
            return new Vector2(1, 1);
        }
        public virtual Vector2 GetOrigin()
        {
            return new Vector2(8, 8);
        }
    }
}
