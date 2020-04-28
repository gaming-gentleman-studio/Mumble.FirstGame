using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    public class NullAction : IAction
    {
        public ActionResult Result => null;

        public bool HasResult()
        {
            return false;
        }
    }
}
