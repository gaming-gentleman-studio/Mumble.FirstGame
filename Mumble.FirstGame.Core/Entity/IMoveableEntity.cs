using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity
{
    public interface IMoveableEntity : IEntity
    {
        IPositionComponent PositionComponent
        {
            get;
        }
    }
}
