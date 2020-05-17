using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Scene.Battle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Movement
{
    public interface IMoveAction : IAction
    {
        IVelocityComponent Velocity { get; }

        IMoveableEntity Entity { get; }

        void CalculateEffect(SceneBoundary boundary);
    }
}
