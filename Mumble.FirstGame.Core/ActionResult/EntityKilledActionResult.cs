using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class EntityKilledActionResult : IActionResult
    {
        public readonly string TargetName;
        public EntityKilledActionResult(string targetName)
        {
            TargetName = targetName;
        }
    }
}
