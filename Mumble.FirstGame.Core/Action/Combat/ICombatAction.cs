using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Combat
{
    public interface ICombatAction : IAction
    {
        public bool EndsEntityTurn();
        public void CalculateEffect();
    }
}
