using Microsoft.Xna.Framework;
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
        public FireWeaponAction HandleMouseClick(IMoveableCombatEntity entity, Vector2 sourcePosition)
        {
            if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
            {
                Direction direction = Mouse.GetState().Position.ToVector2().ToRelativeDirection(sourcePosition);
                return new FireWeaponAction(entity, direction);
            }
            return null;
        }
    }
}
