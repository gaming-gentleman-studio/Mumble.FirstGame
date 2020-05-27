using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
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
                Vector2 directionVector = Mouse.GetState().Position.ToVector2();
                directionVector = new Vector2(directionVector.X - sourcePosition.X, directionVector.Y - sourcePosition.Y);
                directionVector.Normalize();
                Direction direction = new Direction(directionVector.X, directionVector.Y);
                return new FireWeaponAction(entity, direction);
            }
            return null;
        }
    }
}
