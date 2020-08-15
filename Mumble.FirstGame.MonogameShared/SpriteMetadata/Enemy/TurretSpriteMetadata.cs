using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.MonogameShared.Animation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata.Enemy
{
    public class TurretSpriteMetadata : AbstractSpriteMetadata
    {
        protected IEntity _entity { get; }
        private AnimationHandler _animationHandler;
        public TurretSpriteMetadata(IEntity entity)
        {
            _entity = entity;
            _animationHandler = new AnimationHandler(new AnimationHandlerSettings()
            {
                AnimationTypePrecedence = new List<AnimationTypes>()
                {
                    AnimationTypes.Damage,
                    AnimationTypes.Idle
                },
                SpritesheetSettingsMap = new Dictionary<AnimationTypes, SpritesheetSettings>()
                {
                    { AnimationTypes.Damage, new SpritesheetSettings()
                    {
                        FrameDelay = 3,
                        ColorChangeOnly = true,
                        ColorChange = Color.Red
                    } },
                    { AnimationTypes.Idle, new SpritesheetSettings() }
                }
            });
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Turret;
        }
        public override void AnimateAttack()
        {
            return;
        }
        public override void AnimateDamage()
        {
            _animationHandler.AnimateType(AnimationTypes.Damage);
        }
        public override void AnimateMovement(MoveActionResult result)
        {
            return;
        }
        public override Color GetColor()
        {
            return _animationHandler.GetColor();
        }
        public override Rectangle GetSpritesheetRectange(Vector2 mousePosition)
        {
            return _animationHandler.GetSpritesheetRectange(mousePosition, GetPosition());
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
