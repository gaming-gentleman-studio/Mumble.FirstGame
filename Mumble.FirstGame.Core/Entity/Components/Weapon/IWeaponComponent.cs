using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public interface IWeaponComponent
    {
        IVelocityComponent VelocityComponent { get; }
        IDamageComponent DamageComponent { get; }

        bool AbleToFire(int elapsedTicks);
        List<IProjectileEntity> Fire(float sourceX, float sourceY, Direction direction);
    }
}
