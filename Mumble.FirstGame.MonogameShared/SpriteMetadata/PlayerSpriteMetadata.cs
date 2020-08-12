using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.ActionResult;
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
    public class PlayerSpriteMetadata : AbstractSpriteMetadata
    {

        private IEntity _entity;
        private int _animationStep = 0;
        private int _animationDelay = 0;
        private const int MAX_ANIMATION_DELAY = 2;
        private const int MAX_ANIMATION_STEPS = 3;
        private int _damage_flash_count = 0;

        public PlayerSpriteMetadata(Player entity)
        {
            _entity = entity;
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.ImgTheDude;
        }
        public override Rectangle GetSpritesheetRectange()
        {
            Vector2 vec = Mouse.GetState().Position.ToVector2();
            Direction direction = vec.ToRelativeDirection(GetPosition());
            Direction facing = Direction.ToNearest90Angle(direction);
            Rectangle rect = SpriteMetadataUtil.SpritesheetPosByDirection[facing];
            rect.Y = (16 * _animationStep)+_animationStep;
            
            return rect;

        }
        public override void AnimateMovement(MoveActionResult result)
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
        public override Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X,
                _entity.PositionComponent.Y
            );
        }
        public override Vector2 GetScale()
        {
            return new Vector2(_entity.SizeComponent.Scale, _entity.SizeComponent.Scale);
            
        }
        public override void AnimateDamage()
        {
            _damage_flash_count = 3;
        }
        public override Color GetColor()
        {
            if (_damage_flash_count < 1)
            {
                return base.GetColor();
            }
            else
            {
                _damage_flash_count--;
                return Color.Red;

            }

        }
    }
}
