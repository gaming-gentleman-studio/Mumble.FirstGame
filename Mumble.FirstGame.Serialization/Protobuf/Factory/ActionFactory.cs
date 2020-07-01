using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.OnlineAction;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Direction = Mumble.FirstGame.Core.Entity.Components.Velocity.Direction;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public class ActionFactory : IActionFactory
    {
        private IEntityContainer _entityContainer;

        public ActionFactory(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        
        public IAction ToAction(byte[] data,IOwnerIdentifier owner)
        {
            int type = data[0];
            byte[] serializedAction = data.Skip(1).Take(data.Length).ToArray();
            IAction action = new NullAction();
            switch (type)
            {
                case ActionTypeLookup.Move:
                    try
                    {
                        MoveActionDef moveDef = MoveActionDef.Parser.ParseFrom(serializedAction);
                        action = new MoveAction((IMoveableEntity)_entityContainer.GetEntity(moveDef.Id), new Direction(moveDef.Direction.Radians));
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to parse move action");
                    }
                    break;
                case ActionTypeLookup.Fire:
                    try
                    {
                        FireActionDef fireDef = FireActionDef.Parser.ParseFrom(serializedAction);
                        action = new FireWeaponAction((ICombatEntity)_entityContainer.GetEntity(fireDef.Id), new Direction(fireDef.Direction.Radians));
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to parse fire action");

                    }
                    break;
                case ActionTypeLookup.EntitiesCreated:
                    try
                    {
                        SpawnEntityActionDef spawnDef = SpawnEntityActionDef.Parser.ParseFrom(serializedAction);
                        switch (spawnDef.Type)
                        {
                            case (EntityTypeLookup.Player):
                                action = new SpawnPlayerAction(spawnDef.Name, 3, 10, owner);
                                break;
                            default:
                                throw new Exception("Unhandled entity found while attempting to deserialize");
                        }
                            
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to parse spawn action");
                    }
                    break;
                case ActionTypeLookup.ClientRegistration:
                    try
                    {
                        RegisterClientActionDef registerClientDef = RegisterClientActionDef.Parser.ParseFrom(serializedAction);
                        action = new RegisterClientAction();
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to parse client registration action");
                    }
                    break;
                default:
                    throw new Exception("Improper action found while attempting to deserialize");
            }
            return action;
        }

        public IMessage ToProtobufDef(IAction action)
        {
            if (action is IMoveAction)
            {
                IMoveAction move = (IMoveAction)action;
                return new MoveActionDef
                {
                    Id = _entityContainer.GetEntityId(move.Entity),
                    Direction = new Protobuf.Action.Direction
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
                    Id = _entityContainer.GetEntityId(fire.Entity),
                    Direction = new Protobuf.Action.Direction
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
    }
}
