using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    //Sent from UI to logic
    public interface IAction
    {
        public ActionResult Result
        {
            get;
        }
        public bool HasResult();
    }
}
