using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.OnlineAction;
using Mumble.FirstGame.Serialization.OnlineActionResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Server
{
    public class OnlineActionAdapter : IActionAdapter
    {
        private IEntityContainer _entityContainer;
        public OnlineActionAdapter(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
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
