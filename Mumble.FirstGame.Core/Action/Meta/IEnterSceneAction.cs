using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Meta
{
    public interface IEnterSceneAction : IAction
    {
        void CalculateEffect();
    }
}
