using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf
{
    public static class Lookup
    {

        public const int Move = 0;
        public const int Fire = 1;
        public const int EntitiesCreated = 2;

        public static Dictionary<Type, int> TypeToTypeId = new Dictionary<Type, int>()
        {
            { typeof(MoveAction), Move },
            { typeof(MoveActionResult), Move },
            { typeof(FireWeaponAction), Fire },
            { typeof(EntitiesCreatedActionResult), EntitiesCreated }
        };
    }
}
