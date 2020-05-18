using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity
{
    public interface IMoveableEntity : IEntity
    {
        IVelocityComponent VelocityComponent
        {
            get;
        }
    }
}
