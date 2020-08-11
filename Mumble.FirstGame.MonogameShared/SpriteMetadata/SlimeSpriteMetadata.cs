using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.ActionResult;
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
        private int _animationStep = 0;
        private int _animationDelay = 0;
        private const int MAX_ANIMATION_DELAY = 2;
        private const int MAX_ANIMATION_STEPS = 2;

        private int _attackAnimationFrameDelayCnt = 0;
        private const int ATTACK_ANIMATION_ROW = 3;
        private const int ATTACK_ANIMATION_FRAME_DELAY = 7;
        private Direction _facing = Direction.Down;
        private bool isAttacking = false;
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
            
            Rectangle rect = SpriteMetadataUtil.SpritesheetPosByDirection[_facing];
            if (isAttacking)
            {
                _attackAnimationFrameDelayCnt++;
                if (_attackAnimationFrameDelayCnt >= ATTACK_ANIMATION_FRAME_DELAY)
                {
                    _attackAnimationFrameDelayCnt = 0;
                    isAttacking = false;
                }
                rect.Y = (16 * ATTACK_ANIMATION_ROW) + ATTACK_ANIMATION_ROW;

            }
            else
            {
                rect.Y = (16 * _animationStep) + _animationStep;
            }
            

            return rect;

        }
        public override void AnimateMovement(MoveActionResult result)
        {
            if (result.Direction == Direction.None)
            {
                _facing = Direction.ToNearest90Angle(result.Direction);
            }
            _animationDelay++;
            if (_animationDelay >= MAX_ANIMATION_DELAY)
            {
                _animationDelay = 0;
                _animationStep++;
                if (_animationStep > MAX_ANIMATION_STEPS)
                {
                    _animationStep = 0;
                }
            }

        }
        public override void AnimateDamage()
        {
            _damage_flash_count = 3;
        }
        public override void AnimateAttack()
        {
            _animationStep = MAX_ANIMATION_STEPS;
            _animationDelay = 0;
            isAttacking = true;

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
                _entity.PositionComponent.X * SPRITE_PIXEL_SPACE,
                _entity.PositionComponent.Y * SPRITE_PIXEL_SPACE
            );
        }
        public override Vector2 GetScale()
        {
            return new Vector2(_entity.Scale, _entity.Scale);

        }


    }
}
