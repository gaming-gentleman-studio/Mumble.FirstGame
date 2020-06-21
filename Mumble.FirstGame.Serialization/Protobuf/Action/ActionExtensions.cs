using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.OnlineAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Action
{
    public static class ActionExtensions
    {
        public static IMessage ToProtobufDefinition(this IAction action, IEntityContainer entityContainer)
        {
            if (action is IMoveAction)
            {
                IMoveAction move = (IMoveAction)action;
                return new MoveActionDef
                {
                    Id = entityContainer.GetEntityId(move.Entity),
                    Direction = new Direction
                    {
                        Radians = move.Velocity.Direction.Radians
                    }
                };
            }
            else if (action is IFireWeaponAction)
            {
                IFireWeaponAction fire = (IFireWeaponAction)action;
                return new FireActionDef
                {
                    Id = entityContainer.GetEntityId(fire.Entity),
                    Direction = new Direction
                    {
                        Radians = fire.Direction.Radians
                    }
                };
            }
            else if (action is ISpawnEntityAction)
            {
                ISpawnEntityAction spawn = (ISpawnEntityAction)action;
                return new SpawnEntityActionDef
                {
                    Name = spawn.Entity.GetName(),
                    Type = EntityTypeLookup.TypeToTypeId[spawn.Entity.GetType()]
                };
            }
            else if (action is RegisterClientAction)
            {
                RegisterClientAction registration = (RegisterClientAction)action;
                return new RegisterClientActionDef();
            }
            return null;

        }
        public static byte GetTypeByte(this IAction action)
        {
            return (byte)ActionTypeLookup.TypeToTypeId[action.GetType()];
        }
    }
}
