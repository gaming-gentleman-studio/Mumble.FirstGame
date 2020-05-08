using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.AI
{
    public class SimpleAttackAIComponent : ICombatAIComponent
    {
        private Random _randomGenerator;
        public SimpleAttackAIComponent()
        {
            _randomGenerator = new Random();
        }
        public IAttackAction GenerateCombatAction(ICombatEntity source, List<ICombatEntity> potentialTargets)
        {
            int targetIdx = _randomGenerator.Next(0, potentialTargets.Count);
            return new AttackAction(source, potentialTargets[targetIdx]);

        }
    }
}
