using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Entity.Components.AI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity
{
    public interface ICombatAIEntity : ICombatEntity
    {
        ICombatAIComponent CombatAIComponent
        {
            get;
        }
    }
}
