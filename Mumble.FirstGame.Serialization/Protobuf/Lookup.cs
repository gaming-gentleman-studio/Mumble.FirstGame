using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Serialization.OnlineAction;
using Mumble.FirstGame.Serialization.OnlineActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf
{
    public static class ActionTypeLookup
    {

        public const int Move = 0;
        public const int Fire = 1;
        public const int EntitiesCreated = 2;
        public const int EntityDestroyed = 3;
        public const int ClientRegistration = 4;

        public static Dictionary<Type, int> TypeToTypeId = new Dictionary<Type, int>()
        {
            { typeof(MoveAction), Move },
            { typeof(MoveActionResult), Move },
            { typeof(UseWeaponAction), Fire },
            { typeof(EntitiesCreatedActionResult), EntitiesCreated },
            { typeof(SpawnPlayerAction), EntitiesCreated },
            { typeof(EntityDestroyedActionResult), EntityDestroyed },
            { typeof(RegisterClientAction),ClientRegistration },
            { typeof(ClientRegisteredActionResult),ClientRegistration }
        };
    }
    public static class EntityTypeLookup
    {
        public const int Player = 0;
        public const int SimpleBullet = 1;

        public static Dictionary<Type, int> TypeToTypeId = new Dictionary<Type, int>()
        {
            { typeof(Player), Player },
            { typeof(SimpleBullet), SimpleBullet }
        };
    }
}
