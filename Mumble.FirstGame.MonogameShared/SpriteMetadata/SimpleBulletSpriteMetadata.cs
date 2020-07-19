using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    class SimpleBulletSpriteMetadata : ISpriteMetadata
    {
        private IProjectileEntity _entity;
        private const int SCALING = 5;

        public SimpleBulletSpriteMetadata(SimpleBullet entity)
        {
            _entity = entity;
        }
        public Texture2D GetImage(ContentImages container)
        {
            return container.Bullet;
        }
        public float GetRotation()
        {
            return _entity.VelocityComponent.Direction.Radians;

        }
        public Rectangle GetSpritesheetRectange()
        {
            return new Rectangle(0, 0, 16, 16);

        }
        public Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X * 2 * SCALING + 16,
                _entity.PositionComponent.Y * 2 * SCALING + 16
            );
        }
        public Vector2 GetScale()
        {
            return new Vector2(1, 1);

        }
        public Vector2 GetOrigin()
        {
            return Vector2.Zero;
        }

        public void AnimateMovement()
        {
            return;
        }
    }
}
