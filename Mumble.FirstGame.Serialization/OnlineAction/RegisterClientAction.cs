using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Serialization.OnlineActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.OnlineAction
{
    public class RegisterClientAction : IAction
    {
        public List<IActionResult> Results { get; private set; }

        public RegisterClientAction()
        {
            Results = new List<IActionResult>();
        }
        public void CalculateEffect(IOwnerIdentifier owner)
        {
            Results.Add(new ClientRegisteredActionResult(owner));
        }
    }
}
