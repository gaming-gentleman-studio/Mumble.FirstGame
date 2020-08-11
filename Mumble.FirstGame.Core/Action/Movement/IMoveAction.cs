using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.Battle.SceneBoundary;
using Mumble.FirstGame.Core.System.Collision;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Movement
{
    public interface IMoveAction : IAction
    {
        IVelocityComponent Velocity { get; }

        IMoveableEntity Entity { get; }

        void CalculateEffect(ICollisionSystem collisionSystem);
    }
}
