using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class AttackActionResult : IActionResult
    {
        public readonly ICombatEntity Source;
        public readonly ICombatEntity Target;

        public AttackActionResult(ICombatEntity source, ICombatEntity target)
        {
            Source = source;
            Target = target;
        }
    }
}
