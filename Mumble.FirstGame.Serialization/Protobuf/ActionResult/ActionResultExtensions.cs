using Google.Protobuf;
using Google.Protobuf.Collections;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.ActionResult
{
    public static class ActionResultExtensions
    {
        public static IMessage ToProtobufDefinition(this IActionResult result, IEntityContainer entityContainer)
        {
            if (result is MoveActionResult)
            {
                MoveActionResult move = (MoveActionResult)result;
                return new MoveActionResultDef
                {
                    Id = entityContainer.GetEntityId(move.Entity),
                    X = move.XPos,
                    Y = move.YPos,
                    OutOfBounds = move.OutOfBounds
                };
            }
            else if (result is EntitiesCreatedActionResult)
            {
                EntitiesCreatedActionResult entitiesCreatedResult = (EntitiesCreatedActionResult) result;
                EntitiesCreatedActionResultDef message = new EntitiesCreatedActionResultDef();
                foreach (IMoveableEntity entity in entitiesCreatedResult.Entities)
                {
                    var entityDef = new EntitiesCreatedActionResultDef.Types.Entity();
                    entityDef.Id = entityContainer.GetEntityId(entity);
                    entityDef.Direction = new Direction
                    {
                        Radians = entity.VelocityComponent.Direction.Radians
                    };
                    entityDef.X = entity.PositionComponent.X;
                    entityDef.Y = entity.PositionComponent.Y;
                    message.Entities.Add(entityDef);
                }
                return message;
            }
            return null;

        }
        public static byte GetTypeByte(this IActionResult action)
        {
            return (byte)Lookup.TypeToTypeId[action.GetType()];
        }
    }
}
