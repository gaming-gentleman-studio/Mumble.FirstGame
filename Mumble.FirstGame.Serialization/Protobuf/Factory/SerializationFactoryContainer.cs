using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public class SerializationFactoryContainer : ISerializationFactoryContainer
    {

        public IActionFactory ActionFactory { get; private set; }

        public IActionResultFactory ActionResultFactory { get; private set; }

        public IEntityFactory EntityFactory { get; private set; }
        public SerializationFactoryContainer(IActionFactory actionFactory, IActionResultFactory actionResultFactory, IEntityFactory entityFactory)
        {
            ActionFactory = actionFactory;
            ActionResultFactory = actionResultFactory;
            EntityFactory = entityFactory;
        }
    }
}
