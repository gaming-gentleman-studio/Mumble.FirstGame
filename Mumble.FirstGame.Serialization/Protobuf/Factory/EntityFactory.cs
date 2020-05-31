using Google.Protobuf;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Serialization.Protobuf.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public class EntityFactory : IEntityFactory
    {
        public IEntity CreateEntity(EntitiesCreatedActionResultDef.Types.Entity serializedEntity)
        {
            switch (serializedEntity.Type)
            {
                case EntityTypeLookup.Player:
                    IntOwnerIdentifier identifier = new IntOwnerIdentifier(serializedEntity.Owner);
                    return new Player(serializedEntity.Name, serializedEntity.X, serializedEntity.Y,identifier);
                case EntityTypeLookup.SimpleBullet:
                    return new SimpleBullet(serializedEntity.X, serializedEntity.Y, new Direction(serializedEntity.Direction.Radians));
                default:
                    break;
            }
            return null;
                
        }
    }
}
