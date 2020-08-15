using Microsoft.Xna.Framework;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.MonogameShared.SpriteMetadata.Enemy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class SpriteMetadataUtil
    {

        public static Dictionary<Direction, Rectangle> GetSpritesheetPosByDirection(int width, int height)
        {
            return new Dictionary<Direction, Rectangle>()
            {
                {Direction.Down , new Rectangle(0,0,width,height) },
                {Direction.Up, new Rectangle(width,0,width,height) },
                {Direction.Right, new Rectangle(width*2,0,width,height) },
                {Direction.Left, new Rectangle(width*3,0,width,height) }
            };
        }
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
