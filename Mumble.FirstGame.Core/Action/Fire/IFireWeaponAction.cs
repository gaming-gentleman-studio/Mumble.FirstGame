using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Fire
{
    public interface IFireWeaponAction : IAction
    {
        List<IProjectileEntity> CalculateEffect();
    }
}
