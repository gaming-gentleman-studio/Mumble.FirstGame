using Google.Protobuf;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.OnlineAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Action
{
    public static class ActionExtensions
    {
        public static byte GetTypeByte(this IAction action)
        {
            return (byte)ActionTypeLookup.TypeToTypeId[action.GetType()];
        }
    }
}
