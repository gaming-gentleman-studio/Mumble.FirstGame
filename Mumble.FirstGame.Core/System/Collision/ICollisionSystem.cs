using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.System.Collision
{
    public interface ICollisionSystem
    {
        bool HasCollision(IPositionComponent position, IPositionComponent selfPosition, IOwnerIdentifier ownerIdentifier);
        bool HasCollision(IPositionComponent position, IPositionComponent selfPosition);
        IEntity GetEntityByPosition(IPositionComponent position);
    }
}
