using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.AI
{
    public class SimpleAttackPlayerAIComponent : ICombatAIComponent
    {
        public IAction GenerateAction(ICombatEntity source, List<ICombatEntity> potentialTargets)
        {
            //TODO
            ICombatEntity target = potentialTargets[0];
            Direction direction = new Direction(target.PositionComponent.X - source.PositionComponent.X, target.PositionComponent.Y - source.PositionComponent.Y);
            FireWeaponAction action = new FireWeaponAction(source, direction);
            return action;
        }
    }
}
