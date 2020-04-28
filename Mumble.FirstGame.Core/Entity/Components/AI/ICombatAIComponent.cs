using Mumble.FirstGame.Core.Action.Combat;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.AI
{
    public interface ICombatAIComponent : IEntityComponent
    {
        public ICombatAction GenerateCombatAction(ICombatEntity source, List<ICombatEntity> potentialTargets);
    }
}
