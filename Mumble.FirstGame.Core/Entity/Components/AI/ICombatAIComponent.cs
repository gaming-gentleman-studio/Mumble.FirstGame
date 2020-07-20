using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.AI
{
    public interface ICombatAIComponent : IEntityComponent
    {
        IAction GenerateAction(ICombatEntity source, List<ICombatEntity> potentialTargets);
    }
}
