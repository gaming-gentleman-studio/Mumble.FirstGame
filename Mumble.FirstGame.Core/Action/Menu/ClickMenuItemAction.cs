using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.ActionResult.Meta;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Menu
{
    public class ClickMenuItemAction : IClickMenuItemAction
    {
        public List<IActionResult> Results { get; private set; }
        private MenuOption _option;

        public ClickMenuItemAction(MenuOption option)
        {
            _option = option;
        }

        public void CalculateEffect(IOwnerIdentifier owner,IScene scene)
        {
            Results = scene.Update(new Dictionary<IOwnerIdentifier, List<IAction>>()
            {
                { owner, new List<IAction>()
                {
                    _option.Action
                } }
            },0);
        }
    }
}
