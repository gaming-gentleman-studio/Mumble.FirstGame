using Mumble.FirstGame.Core.Entity.Components.Damage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Projectile
{
    public interface IProjectileEntity : IMoveableEntity
    {
        IDamageComponent DamageComponent
        {
            get;
        }
    }
}
