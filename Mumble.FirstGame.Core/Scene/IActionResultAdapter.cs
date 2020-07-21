using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene
{
    public interface IActionResultAdapter
    {
        bool CanHandleActionResult(IActionResult result);
        List<IAction> HandleActionResult(IActionResult result);
    }
}
