using Google.Protobuf;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.OnlineActionResult;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using Mumble.FirstGame.Serialization.Protobuf.ActionResult;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public class ActionResultFactory : IActionResultFactory
    {
        private IEntityContainer _entityContainer;
        private IEntityFactory _entityFactory;
        public ActionResultFactory(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
            _entityFactory = new EntityFactory();
        }

        public IMessage ToProtobufDef(IActionResult result)
        {
            if (result is MoveActionResult)
            {
                MoveActionResult move = (MoveActionResult)result;
                return new MoveActionResultDef
                {
                    Id = _entityContainer.GetEntityId(move.Entity),
                    X = move.XPos,
                    Y = move.YPos,
                    OutOfBounds = move.OutOfBounds
                };
            }
            else if (result is EntitiesCreatedActionResult)
            {
                EntitiesCreatedActionResult entitiesCreatedResult = (EntitiesCreatedActionResult)result;
                EntitiesCreatedActionResultDef message = new EntitiesCreatedActionResultDef();
                foreach (IMoveableEntity entity in entitiesCreatedResult.Entities)
                {
                    var entityDef = new EntitiesCreatedActionResultDef.Types.Entity();
                    entityDef.Type = EntityTypeLookup.TypeToTypeId[entity.GetType()];
                    entityDef.Id = _entityContainer.GetEntityId(entity);
                    entityDef.Direction = new Protobuf.Action.Direction
                    {
                        Radians = entity.VelocityComponent.Direction.Radians
                    };
                    entityDef.X = entity.PositionComponent.X;
                    entityDef.Y = entity.PositionComponent.Y;
                    if (entity.OwnerIdentifier.PlayerOwned())
                    {
                        entityDef.Owner = ((IntOwnerIdentifier)entity.OwnerIdentifier).Id;
                    }
                    else
                    {
                        entityDef.Owner = 0;
                    }
                    message.Entities.Add(entityDef);
                }
                return message;
            }
            else if (result is EntityDestroyedActionResult)
            {
                EntityDestroyedActionResult destroyedResult = (EntityDestroyedActionResult)result;
                return new EntityDestroyedActionResultDef
                {
                    Id = _entityContainer.GetEntityId(destroyedResult.Entity)
                };
            }
            else if (result is ClientRegisteredActionResult)
            {
                ClientRegisteredActionResult registered = (ClientRegisteredActionResult)result;
                return new ClientRegisteredActionResultDef
                {
                    OwnerId = ((IntOwnerIdentifier)registered.OwnerIdentifier).Id
                };
            }
            return null;

        }

        public IActionResult ToResult(byte[] data)
        {
            int type = data[0];
            byte[] serializedResult = data.Skip(1).Take(data.Length).ToArray();
            if (serializedResult.Length < 1)
            {
                return null;
            }
            IActionResult result = null;
            switch (type)
            {
                case ActionTypeLookup.Move:
                    try
                    {
                        MoveActionResultDef moveDef = MoveActionResultDef.Parser.ParseFrom(serializedResult);
                        result = new MoveActionResult(_entityContainer.GetEntity(moveDef.Id,true), moveDef.X, moveDef.Y, moveDef.OutOfBounds);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Failed to parse Move result: "+ex.Message);
                    }
                    
                    break;
                case ActionTypeLookup.EntitiesCreated:
                    try
                    {
                        EntitiesCreatedActionResultDef createdDef = EntitiesCreatedActionResultDef.Parser.ParseFrom(serializedResult);
                        List<IEntity> entities = new List<IEntity>();
                        foreach (EntitiesCreatedActionResultDef.Types.Entity entityDef in createdDef.Entities)
                        {
                            IEntity entity;
                            if (_entityContainer.HasEntity(entityDef.Id))
                            {
                                entity = _entityContainer.GetEntity(entityDef.Id);
                            }
                            else
                            {
                                entity = _entityFactory.CreateEntity(entityDef);
                                _entityContainer.AddEntity(entityDef.Id, entity);
                            }
                            
                            entities.Add(entity);
                        }
                        result = new EntitiesCreatedActionResult(entities);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Failed to parse Entities created result: "+ex.Message);
                    }
                    break;
                case ActionTypeLookup.EntityDestroyed:
                    try
                    {
                        EntityDestroyedActionResultDef destroyedDef = EntityDestroyedActionResultDef.Parser.ParseFrom(serializedResult);
                        result = new EntityDestroyedActionResult(_entityContainer.GetEntity(destroyedDef.Id,true));
                    }
                    catch (Exception ex)
                    {
                        //Debug.WriteLine("Failed to parse entity destroyed result: "+ex.Message);
                    }
                    break;
                case ActionTypeLookup.ClientRegistration:
                    try
                    {
                        ClientRegisteredActionResultDef registrationDef = ClientRegisteredActionResultDef.Parser.ParseFrom(serializedResult);
                        result = new ClientRegisteredActionResult(new IntOwnerIdentifier(registrationDef.OwnerId));
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to parse Client Registered result");
                    }
                    break;
                default:
                    throw new Exception("Improper action found while attempting to deserialize");
            }
            return result;
        }
    }
}
