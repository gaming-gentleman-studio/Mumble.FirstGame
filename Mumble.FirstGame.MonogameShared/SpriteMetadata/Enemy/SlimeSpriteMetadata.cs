using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.MonogameShared.Animation;
using Mumble.FirstGame.MonogameShared.SpriteMetadata.Enemy;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class SlimeSpriteMetadata : AbstractSpriteMetadata
    {

        protected IEntity _entity { get;  }
        private AnimationHandler _animationHandler;

        public SlimeSpriteMetadata(IEntity entity)
        {
            _entity = entity;
            _animationHandler = new AnimationHandler(new AnimationHandlerSettings()
            {
                SpritesheetSettingsMap = new Dictionary<AnimationTypes, SpritesheetSettings>()
                {
                    { AnimationTypes.Attack, new SpritesheetSettings()
                    {
                        FrameDelay = 7,
                        Rows = new List<int>()
                        {
                            3
                        },
                        
                    } },
                    { AnimationTypes.Move, new SpritesheetSettings()
                    {
                        FrameDelay = 2,
                        Rows = new List<int>()
                        {
                            0,
                            1,
                            2
                        }
                    } },
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
            return container.Slime;
        }
        public override void AnimateAttack()
        {
            _animationHandler.AnimateType(AnimationTypes.Attack);
        }
        public override void AnimateDamage()
        {
            _animationHandler.AnimateType(AnimationTypes.Damage);
        }
        public override void AnimateMovement(MoveActionResult result)
        {
            _animationHandler.AnimateFacing(result.Direction);
            _animationHandler.AnimateType(AnimationTypes.Move);
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
