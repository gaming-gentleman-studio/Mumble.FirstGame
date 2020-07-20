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
        CollisionResult HasCollision(IPositionComponent position, IPositionComponent selfPosition, IOwnerIdentifier ownerIdentifier);
        IEntity GetEntityByPosition(IPositionComponent position);
    }
    public class CollisionResult
    {
        public bool HasCollision;
        public IEntity CollidedEntity = null;
    }
}
