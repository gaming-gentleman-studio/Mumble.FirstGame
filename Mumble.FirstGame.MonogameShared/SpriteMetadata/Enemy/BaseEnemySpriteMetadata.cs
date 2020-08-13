﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata.Enemy
{
    public abstract class BaseEnemySpriteMetadata : AbstractSpriteMetadata
    {
        protected abstract IEntity _entity { get; }
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

        public override Rectangle GetSpritesheetRectange(Vector2 mousePosition)
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
                _entity.PositionComponent.X,
                _entity.PositionComponent.Y
            );
        }
        public override Vector2 GetScale()
        {
            return new Vector2(_entity.SizeComponent.Scale, _entity.SizeComponent.Scale);

        }

    }
}
