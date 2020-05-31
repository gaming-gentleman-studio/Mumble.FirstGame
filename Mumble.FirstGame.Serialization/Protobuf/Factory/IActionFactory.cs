using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public interface IActionFactory
    {
        IAction Create(byte[] data, IOwnerIdentifier owner);
    }
}
