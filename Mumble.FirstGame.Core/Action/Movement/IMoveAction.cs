using Mumble.FirstGame.Core.Scene.Battle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Movement
{
    public interface IMoveAction : IAction
    {
        MoveAction.DirectionValues Direction
        {
            get;
        }

        void CalculateEffect(SceneBoundary boundary);
    }
}
