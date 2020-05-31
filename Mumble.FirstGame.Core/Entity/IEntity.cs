using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity
{
    public interface IEntity
    {
        IPositionComponent PositionComponent
        {
            get;
        }
        string GetName();
        IOwnerIdentifier OwnerIdentifier { get; }
    }
}
