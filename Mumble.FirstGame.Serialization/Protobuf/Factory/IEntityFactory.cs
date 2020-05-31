using Google.Protobuf;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Serialization.Protobuf.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public interface IEntityFactory
    {
        IEntity CreateEntity(EntitiesCreatedActionResultDef.Types.Entity serializedEntity);
    }
}
