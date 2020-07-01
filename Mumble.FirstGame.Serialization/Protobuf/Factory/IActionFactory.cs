using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public interface IActionFactory
    {
        IAction ToAction(byte[] data, IOwnerIdentifier owner);
        IMessage ToProtobufDef(IAction action);
    }
}
