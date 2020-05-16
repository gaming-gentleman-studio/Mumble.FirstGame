using Mumble.FirstGame.Core.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    public class NullAction : IAction
    {
        public IActionResult Result => null;

        public bool HasResult()
        {
            return false;
        }
    }
}
