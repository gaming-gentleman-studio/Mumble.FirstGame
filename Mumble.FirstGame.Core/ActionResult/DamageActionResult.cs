using System;
using System.Collections.Generic;
using System.Text;
using Mumble.FirstGame.Core.ActionResult;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class DamageActionResult : IActionResult
    {
        public readonly string TargetName;
        public readonly int DamageTaken;
        public DamageActionResult(string targetName, int damageTaken)
        {
            TargetName = targetName;
            DamageTaken = damageTaken;
        }
    }
}
