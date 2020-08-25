using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Meta;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Trigger
{
    public class ReturnToMenuOnPlayerDeathTrigger : ITrigger
    {
        public bool CanHandleActionResult(IActionResult result)
        {
            if (result is EntityDestroyedActionResult)
            {
                EntityDestroyedActionResult destroyedResult = (EntityDestroyedActionResult)result;
                return (destroyedResult.Entity is Player);
            }
            return false;
        }

        public List<IAction> HandleActionResult(IActionResult result)
        {
            return new List<IAction>()
            {
                new LoadSceneAction()
            };
        }

        public bool TryHandleAction(IAction action, IOwnerIdentifier owner)
        {
            return false;
        }
    }
}
