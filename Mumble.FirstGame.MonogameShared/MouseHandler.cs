﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    public class MouseHandler
    {
        public UseWeaponAction HandleMouseClick(IMoveableCombatEntity entity, Vector2 sourcePosition)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Direction direction = Mouse.GetState().Position.ToVector2().ToRelativeDirection(sourcePosition);
                return new UseWeaponAction(entity, direction);
            }
            return null;
        }
    }
}
