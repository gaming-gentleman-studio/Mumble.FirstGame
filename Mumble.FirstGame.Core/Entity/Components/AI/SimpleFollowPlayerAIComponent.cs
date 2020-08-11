using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Action.Fire;
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
        private const float _damageDistance = 3f;
        public IAction GenerateAction(ICombatEntity source, List<ICombatEntity> players)
        {
            //first one for now
            IPositionComponent playerPosition = players[0].PositionComponent;
            IPositionComponent sourcePosition = source.PositionComponent;
            float a = playerPosition.X - sourcePosition.X;
            float b = playerPosition.Y - sourcePosition.Y;
            double c = Math.Sqrt((a * a) + (b * b));
            if (c < _damageDistance)
            {
                return new AttackAction(source, players[0]);
            }
            else
            {
                Direction direction = new Direction(a, b);
                return new MoveAction(source, direction);
            }

        }
    }
}
