using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.ActionResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public class ActionResultFactory : IActionResultFactory
    {
        private IEntityContainer _entityContainer;
        public ActionResultFactory(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }


        public IActionResult Create(byte[] data)
        {
            int type = data[0];
            byte[] serializedResult = data.Skip(1).Take(data.Length).ToArray();
            IActionResult result = null;
            switch (type)
            {
                case Lookup.Move:
                    MoveActionResultDef moveDef = MoveActionResultDef.Parser.ParseFrom(serializedResult);
                    result = new MoveActionResult(_entityContainer.GetEntity(moveDef.Id),moveDef.X,moveDef.Y,moveDef.OutOfBounds);
                    break;
                case Lookup.EntitiesCreated:
                    EntitiesCreatedActionResultDef createdDef = EntitiesCreatedActionResultDef.Parser.ParseFrom(serializedResult);
                    List<IEntity> entities = new List<IEntity>();
                    foreach(EntitiesCreatedActionResultDef.Types.Entity entityDef in createdDef.Entities)
                    {
                        SimpleBullet bullet = new SimpleBullet(entityDef.X, entityDef.Y,0,Lookup.SerializedToDirectionMap[entityDef.Direction],0);
                        _entityContainer.AddEntity(entityDef.Id, bullet);
                        entities.Add(bullet);
                    }
                    result = new EntitiesCreatedActionResult(entities);
                    break;
                default:
                    throw new Exception("Improper action found while attempting to deserialize");
            }
            return result;
        }
    }
}
