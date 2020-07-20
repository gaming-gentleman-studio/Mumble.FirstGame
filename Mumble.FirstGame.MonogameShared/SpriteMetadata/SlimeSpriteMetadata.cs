using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class SlimeSpriteMetadata : AbstractSpriteMetadata
    {

        private IEntity _entity;
        private const int SCALING = 5;
        private int _animationStep = 0;
        private int _animationDelay = 0;
        private const int MAX_ANIMATION_DELAY = 2;
        private const int MAX_ANIMATION_STEPS = 3;

        private Direction _facing = Direction.Down;
        private int _facing_change_step = 0;
        private const int FACING_CHANGE_FRAMES = 15;

        private int _damage_flash_count = 0;

        public SlimeSpriteMetadata(Slime entity)
        {
            _entity = entity;
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Slime;
        }
        public override Rectangle GetSpritesheetRectange()
        {
            _facing_change_step++;
            if (_facing_change_step >= FACING_CHANGE_FRAMES)
            {
                Direction direction = Direction.GetRandom90Direction();
                _facing = Direction.ToNearest90Angle(direction);
                _facing_change_step = 0;
            }
            
            Rectangle rect = SpriteMetadataUtil.SpritesheetPosByDirection[_facing];
            rect.Y = (16 * _animationStep) + _animationStep;

            return rect;

        }
        public override void AnimateMovement()
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
        public override Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X * 2 * SCALING,
                _entity.PositionComponent.Y * 2 * SCALING
            );
        }
        public override Vector2 GetScale()
        {
            return new Vector2(2, 2);

        }


    }
}
