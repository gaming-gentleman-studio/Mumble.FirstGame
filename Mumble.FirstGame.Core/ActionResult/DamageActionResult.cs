using System;
using System.Collections.Generic;
using System.Text;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class DamageActionResult : IActionResult
    {
        public readonly ICombatEntity Entity;
        public readonly int DamageTaken;
        public DamageActionResult(ICombatEntity target, int damageTaken)
        {
            Entity = target;
            DamageTaken = damageTaken;
        }
    }
}
