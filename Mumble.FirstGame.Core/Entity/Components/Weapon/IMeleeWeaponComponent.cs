using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Weapon
{
    public interface IMeleeWeaponComponent : IWeaponComponent
    {
        IActionResult Attack(ICombatEntity target);
    }
}
