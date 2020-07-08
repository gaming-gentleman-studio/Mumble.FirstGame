using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class PlayerSpriteMetadata : ISpriteMetadata
    {

        private IEntity _entity;
        private const int SCALING = 5;
        private int _animationStep = 0;
        private int _animationDelay = 0;
        private const int MAX_ANIMATION_DELAY = 2;
        private const int MAX_ANIMATION_STEPS = 3;

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
            Vector2 vec = Mouse.GetState().Position.ToVector2();
            Direction direction = vec.ToRelativeDirection(GetPosition());
            Direction facing = Direction.ToNearest90Angle(direction);
            Rectangle rect = SpriteMetadataUtil.SpritesheetPosByDirection[facing];
            rect.Y = (16 * _animationStep)+_animationStep;
            
            return rect;

        }
        public void Animate()
        {
            _animationDelay++;
            if (_animationDelay > MAX_ANIMATION_DELAY - 1)
            {
                _animationDelay = 0;
                _animationStep++;
                if (_animationStep > MAX_ANIMATION_STEPS - 1)
                {
                    _animationStep = 0;
                }
            }

        }
        public Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X * 2 * SCALING,
                _entity.PositionComponent.Y * 2 * SCALING
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
