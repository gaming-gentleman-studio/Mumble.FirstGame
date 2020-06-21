using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Serialization.OnlineAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Server
{
    public class OnlineActionIntercepter : IActionInterceptor
    {
        public OnlineActionIntercepter()
        {

        }
        public bool TryHandleAction(IAction action,IOwnerIdentifier owner)
        {
            if (action is RegisterClientAction)
            {
                ((RegisterClientAction)action).CalculateEffect(owner);
                return true;
            }
            return false;
        }
    }
}
