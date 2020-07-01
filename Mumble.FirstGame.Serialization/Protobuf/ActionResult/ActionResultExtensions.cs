using Google.Protobuf;
using Google.Protobuf.Collections;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.OnlineActionResult;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.ActionResult
{
    public static class ActionResultExtensions
    {
        public static byte GetTypeByte(this IActionResult action)
        {
            return (byte)ActionTypeLookup.TypeToTypeId[action.GetType()];
        }
    }
}
