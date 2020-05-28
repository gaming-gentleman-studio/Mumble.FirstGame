using Mumble.FirstGame.Core.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    public class NullAction : IAction
    {
        public List<IActionResult> Results => new List<IActionResult>();

        public bool HasResult()
        {
            return false;
        }
    }
}
