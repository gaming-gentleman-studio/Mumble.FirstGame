using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    class SimpleBulletSpriteMetadata : AbstractSpriteMetadata
    {
        private IProjectileEntity _entity;
        private const int SCALING = 5;

        public SimpleBulletSpriteMetadata(SimpleBullet entity)
        {
            _entity = entity;
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Bullet;
        }
        public override float GetRotation()
        {
            return _entity.VelocityComponent.Direction.Radians;

        }
        public override Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X * 2 * SCALING,
                _entity.PositionComponent.Y * 2 * SCALING
            );
        }
        public override Vector2 GetOrigin()
        {
            return new Vector2(8, 8);
        }
    }
}
