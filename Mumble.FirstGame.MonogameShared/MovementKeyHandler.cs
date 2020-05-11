using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    internal class MovementKeyHandler
    {
        public MoveAction HandleKeyPress(IMoveableCombatEntity entity)
        {

            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                return new MoveAction(entity,MoveAction.DirectionValues.Down);
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                return new MoveAction(entity,MoveAction.DirectionValues.Left);
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                return new MoveAction(entity,MoveAction.DirectionValues.Up);
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                return new MoveAction(entity,MoveAction.DirectionValues.Right);
            }
            else return null;
        }
        

    }
}
