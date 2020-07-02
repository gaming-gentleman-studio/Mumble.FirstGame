using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class PlayerSpriteMetadata : ISpriteMetadata
    {

        private IEntity _entity;
        private const int _scaling = 5;

        public PlayerSpriteMetadata(Player entity)
        {
            _entity = entity;
        }
        public Texture2D GetImage(ContentImages container)
        {
            return container.ImgTheDude;
        }
        public float GetRotation()
        {
            return 0;

        }
        public Rectangle GetSpritesheetRectange()
        {
            return SpriteMetadataUtil.SpritesheetPosByDirection[_entity.PositionComponent.Facing];

        }
        public Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X * 2 * _scaling,
                _entity.PositionComponent.Y * 2 * _scaling
            );
        }
        public Vector2 GetScale()
        {
            return new Vector2(2, 2);
            
        }
        public Vector2 GetOrigin()
        {
            return Vector2.Zero;
        }
    }
}
