using Mumble.FirstGame.Core.ActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    //Sent from UI to logic
    public interface IAction
    {
        List<IActionResult> Results
        {
            get;
        }
        bool HasResult();

        
    }
}
