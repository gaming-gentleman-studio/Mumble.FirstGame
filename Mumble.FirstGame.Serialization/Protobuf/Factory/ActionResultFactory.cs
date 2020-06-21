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


        public IActionResult Create(byte[] data)
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
                            IEntity entity = _entityFactory.CreateEntity(entityDef);
                            _entityContainer.AddEntity(entityDef.Id, entity);
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
