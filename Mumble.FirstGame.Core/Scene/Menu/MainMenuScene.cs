using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Menu
{
    public class MainMenuScene : IScene
    {
        public bool IsSceneActive()
        {
            throw new NotImplementedException();
        }

        public List<IActionResult> Update(Dictionary<IOwnerIdentifier, List<IAction>> actions, int elapsedTicks)
        {
            throw new NotImplementedException();
        }
    }
}
