using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Menu
{
    public interface IRequestMenuOptionsAction : IAction
    {
        void CalculateEffect();
    }
}
