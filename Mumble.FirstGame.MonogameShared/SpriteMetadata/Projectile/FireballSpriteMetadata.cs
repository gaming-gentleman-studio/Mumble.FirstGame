using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata.Projectile
{
    class FireballSpriteMetadata : AbstractSpriteMetadata
    {
        private IProjectileEntity _entity;

        public FireballSpriteMetadata(Fireball entity)
        {
            _entity = entity;
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Fireball;
        }
        public override float GetRotation()
        {
            return _entity.VelocityComponent.Direction.Radians;

        }
        public override Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X,
                _entity.PositionComponent.Y
            );
        }
        public override Vector2 GetOrigin()
        {
            return new Vector2(8, 8);
        }
    }
}
