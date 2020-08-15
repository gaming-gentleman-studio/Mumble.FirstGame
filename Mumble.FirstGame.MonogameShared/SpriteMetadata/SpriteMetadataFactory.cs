using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.MonogameShared.SpriteMetadata.Enemy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class SpriteMetadataFactory
    {
        public static AbstractSpriteMetadata CreateSpriteMetadata(IEntity entity)
        {
            if (entity is Player)
            {
                return new PlayerSpriteMetadata((Player)entity);
            }
            else if (entity is SimpleBullet)
            {
                return new SimpleBulletSpriteMetadata((SimpleBullet)entity);
            }
            else if (entity is Slime)
            {
                return new SlimeSpriteMetadata(entity);
            }
            else if (entity is Turret)
            {
                return new TurretSpriteMetadata(entity);
            }
            else
            {
                return null;
            }
        }
    }
}
