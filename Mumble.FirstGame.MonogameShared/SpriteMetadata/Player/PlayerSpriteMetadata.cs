using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.MonogameShared.Animation;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static Mumble.FirstGame.MonogameShared.Animation.SpritesheetSettings;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata
{
    public class PlayerSpriteMetadata : AbstractSpriteMetadata
    {

        private IEntity _entity;
        private AnimationHandler _animationHandler;

        public PlayerSpriteMetadata(Player entity)
        {
            _entity = entity;
            _animationHandler = new AnimationHandler(new AnimationHandlerSettings()
            {
                SpritesheetSettingsMap = new Dictionary<AnimationTypes, SpritesheetSettings>()
                {
                    { AnimationTypes.Move, new SpritesheetSettings()
                    {
                        FrameDelay = 3,
                        FacingBasis = FacingBasisEnum.Mouse,
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
                    { AnimationTypes.Idle, new SpritesheetSettings(){
                        CellWidth = 16,
                        CellHeight = 16
                    }}
                },
                AnimationTypePrecedence = new List<AnimationTypes>()
                {
                    AnimationTypes.Damage,
                    AnimationTypes.Move,
                    AnimationTypes.Idle
                }
            });
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.ImgTheDude;
        }
        public override Rectangle GetSpritesheetRectange(Vector2 mousePosition)
        {
            return _animationHandler.GetSpritesheetRectange(mousePosition, GetPosition());

        }
        public override void AnimateMovement(MoveActionResult result)
        {
            _animationHandler.AnimateType(AnimationTypes.Move);

        }
        public override Vector2 GetPosition()
        {
            return new Vector2(
                _entity.PositionComponent.X,
                _entity.PositionComponent.Y
            );
        }
        public override Vector2 GetOrigin()
        {
            return new Vector2(8, 8);
        }
        public override Vector2 GetScale()
        {
            return new Vector2(_entity.SizeComponent.Scale, _entity.SizeComponent.Scale);
            
        }
        public override void AnimateDamage()
        {
            _animationHandler.AnimateType(AnimationTypes.Damage);
        }
        public override Color GetColor()
        {
            return _animationHandler.GetColor();

        }
    }
}
