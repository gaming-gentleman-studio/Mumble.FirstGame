using Microsoft.Xna.Framework;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.MonogameShared.Animation;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using static Mumble.FirstGame.MonogameShared.Animation.SpritesheetSettings;

namespace Mumble.FirstGame.MonogameShared
{
    public enum AnimationTypes
    {
        Attack,
        Move,
        Damage,
        Idle
    }

    public class AnimationHandler
    {
        private readonly AnimationHandlerSettings _settings;


        private Dictionary<AnimationTypes, bool> _enabled = new Dictionary<AnimationTypes, bool>()
        {
            { AnimationTypes.Attack, false },
            { AnimationTypes.Move, false },
            { AnimationTypes.Damage, false },
            { AnimationTypes.Idle, true }
        };
        private Dictionary<AnimationTypes, int> _rowStep = new Dictionary<AnimationTypes, int>()
        {
            { AnimationTypes.Attack, 0 },
            { AnimationTypes.Move, 0 },
            { AnimationTypes.Damage, 0 },
            { AnimationTypes.Idle, 0}
        };
        private Dictionary<AnimationTypes, int> _delayCnt = new Dictionary<AnimationTypes, int>()
        {
            { AnimationTypes.Attack, 0 },
            { AnimationTypes.Move, 0 },
            { AnimationTypes.Damage,0 },
            { AnimationTypes.Idle, 0 }
        };

        private Direction _movedFacing = Direction.Down;

        public AnimationHandler(AnimationHandlerSettings settings)
        {
            _settings = settings;
        }
        public Rectangle GetSpritesheetRectange(Vector2 mousePosition, Vector2 selfPosition)
        {
            AnimateType(AnimationTypes.Idle);
            foreach (AnimationTypes type in _settings.AnimationTypePrecedence)
            {
                SpritesheetSettings settings = _settings.SpritesheetSettingsMap[type];
                Rectangle rect = GetColumnRectangle(mousePosition, selfPosition,settings);

                if (_enabled[type] && !settings.ColorChangeOnly)
                {
                    rect.Y = (settings.CellHeight * settings.Rows[_rowStep[type]]) + settings.Rows[_rowStep[type]];

                    //handle frame delays
                    _delayCnt[type]++;
                    if (_delayCnt[type] > settings.FrameDelay)
                    {
                        _delayCnt[type] = 0;
                        _rowStep[type]++;
                        _enabled[type] = false;
                    }

                    return rect;
                }
            }
            throw new Exception("No enabled animation found");

        }
        private Rectangle GetColumnRectangle(Vector2 mousePosition, Vector2 selfPosition,SpritesheetSettings settings)
        {
            Rectangle rect;
            if (settings.FacingBasis == FacingBasisEnum.Mouse)
            {
                Direction direction = mousePosition.ToRelativeDirection(selfPosition);
                Direction facing = Direction.ToNearest90Angle(direction);
                rect = SpriteMetadataUtil.GetSpritesheetPosByDirection(settings.CellWidth,settings.CellHeight)[facing];
            }
            else if (settings.FacingBasis == FacingBasisEnum.PastMovement)
            {
                Direction facing = Direction.ToNearest90Angle(_movedFacing);
                rect = SpriteMetadataUtil.GetSpritesheetPosByDirection(settings.CellWidth,settings.CellHeight)[facing];
            }
            else
            {
                rect = new Rectangle(0, 0, settings.CellWidth, settings.CellHeight);
            }
            return rect;
        }
        public void AnimateFacing(Direction movedFacingDirection)
        {
            _movedFacing = movedFacingDirection;
        }
        public void AnimateType(AnimationTypes type)
        {
            SpritesheetSettings settings = _settings.SpritesheetSettingsMap[type];
            if (!settings.ColorChangeOnly)
            {
                if (_rowStep[type] > settings.Rows.Count-1)
                {
                    _rowStep[type] = 0;
                }
            }
            _enabled[type] = true;
        }
        public Color GetColor()
        {
            foreach (AnimationTypes type in _settings.AnimationTypePrecedence)
            {
                SpritesheetSettings settings = _settings.SpritesheetSettingsMap[type];
                if (_enabled[type] && settings.ColorChangeOnly)
                {
                    //handle frame delays
                    _delayCnt[type]++;
                    if (_delayCnt[type] >= settings.FrameDelay)
                    {
                        _delayCnt[type] = 0;
                        _enabled[type] = false;
                    }
                    return settings.ColorChange;
                }
            }
            return Color.DarkGray;
        }
    }
}
