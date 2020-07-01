﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public interface IFactoryContainer
    {
        IActionFactory ActionFactory { get; }
        IActionResultFactory ActionResultFactory { get; }
        IEntityFactory EntityFactory { get;  }
    }
}