using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    //Sent from UI to logic
    public interface IAction
    {
        ActionResult Result
        {
            get;
        }
        bool HasResult();

        
    }
}
