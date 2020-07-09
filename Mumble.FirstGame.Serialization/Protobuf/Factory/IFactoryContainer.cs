using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public interface ISerializationFactoryContainer
    {
        IActionFactory ActionFactory { get; }
        IActionResultFactory ActionResultFactory { get; }
        IEntityFactory EntityFactory { get;  }
    }
}
