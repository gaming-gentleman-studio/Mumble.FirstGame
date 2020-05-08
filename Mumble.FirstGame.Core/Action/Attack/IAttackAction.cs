using Mumble.FirstGame.Core.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Attack
{
    public interface IAttackAction : IAction
    {
        //probably not needed? leaving for now
        bool EndsEntityTurn();
        void CalculateEffect();
    }
}
