using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.AI
{
    public class SimpleFollowPlayerAIComponent : ICombatAIComponent
    {
        public IAction GenerateAction(ICombatEntity source, List<ICombatEntity> players)
        {
            //first one for now
            IPositionComponent playerPosition = players[0].PositionComponent;
            IPositionComponent sourcePosition = source.PositionComponent;
            Direction direction = new Direction(playerPosition.X - sourcePosition.X, playerPosition.Y - sourcePosition.Y);
            return new MoveAction(source, direction);
        }
    }
}
