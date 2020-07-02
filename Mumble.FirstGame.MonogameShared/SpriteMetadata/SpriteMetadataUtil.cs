using Microsoft.Xna.Framework;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class SpriteMetadataUtil
    {

        public static Dictionary<Direction, Rectangle> SpritesheetPosByDirection = new Dictionary<Direction, Rectangle>()
        {
            {Direction.Down , new Rectangle(0,0,16,16) },
            {Direction.Up, new Rectangle(16,0,16,16) },
            {Direction.Right, new Rectangle(32,0,16,16) },
            {Direction.Left, new Rectangle(48,0,16,16) }
        };

        public static ISpriteMetadata CreateSpriteMetadata(IEntity entity)
        {
            if (entity is Player)
            {
                return new PlayerSpriteMetadata((Player)entity);
            }
            else if (entity is SimpleBullet)
            {
                return new SimpleBulletSpriteMetadata((SimpleBullet)entity);
            }
            else
            {
                return null;
            }
        }
    }
}
