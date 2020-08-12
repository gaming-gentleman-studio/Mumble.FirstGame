using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Policy;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    internal class MovementKeyHandler
    {

        private readonly Dictionary<Keys, Direction> _map = new Dictionary<Keys, Direction>()
        {
            { Keys.Down, Direction.Down },
            { Keys.S, Direction.Down },
            { Keys.Up, Direction.Up },
            { Keys.W, Direction.Up },
            { Keys.Left, Direction.Left },
            { Keys.A, Direction.Left },
            { Keys.Right, Direction.Right },
            { Keys.D, Direction.Right }
        };
        public IMoveAction HandleKeyPress(IMoveableCombatEntity entity)
        {
            List<IMoveAction> moveActions = new List<IMoveAction>();
            Keys[] keys = Keyboard.GetState().GetPressedKeys();
            Direction direction = Direction.None;
            if (keys.Length > 0)
            {
                
                foreach (Keys key in keys)
                {
                    if (_map.ContainsKey(key))
                    {
                        direction = Direction.Merge(direction, _map[key]);
                    }
                }
               
            }

            if (GamePad.GetState(PlayerIndex.One).DPad.Down == ButtonState.Pressed)
            {
                direction = Direction.Merge(direction, Direction.Down);
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Left == ButtonState.Pressed)
            {
                direction = Direction.Merge(direction, Direction.Left);
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Up == ButtonState.Pressed)
            {
                direction = Direction.Merge(direction, Direction.Up);
            }
            if (GamePad.GetState(PlayerIndex.One).DPad.Right == ButtonState.Pressed)
            {
                direction = Direction.Merge(direction, Direction.Right);
            }
            if (direction != Direction.None)
            {
                return new MoveAction(entity, direction);
            }
            return null;
        }
        

    }
}
