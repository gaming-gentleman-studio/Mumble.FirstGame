using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public interface IRangedWeaponComponent : IWeaponComponent
    {
        IVelocityComponent VelocityComponent { get; }
        IAction Attack(float sourceX, float sourceY, Direction direction, IOwnerIdentifier ownerIdentifier);


    }
}
