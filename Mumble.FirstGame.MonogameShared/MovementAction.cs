using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    internal class MovementKeyHandler
    {
        public MoveAction HandleKeyPress()
        {

            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                return new MoveAction(MoveAction.DirectionValues.Down);
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                return new MoveAction(MoveAction.DirectionValues.Left);
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                return new MoveAction(MoveAction.DirectionValues.Up);
            }
            else if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                return new MoveAction(MoveAction.DirectionValues.Right);
            }
            else return null;
        }
        

    }
}
