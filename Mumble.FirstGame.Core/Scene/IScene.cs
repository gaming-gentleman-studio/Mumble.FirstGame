using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.Battle.SceneBoundary;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene
{
    public interface IScene
    {
        IEntityContainer EntityContainer { get; }
        ISceneBoundary Boundary { get; }
        // This dictionary is a little too exotic and hard to understand imo, maybe make an object of it?
        List<IActionResult> Update(Dictionary<IOwnerIdentifier, List<IAction>> actions,int elapsedTicks);
        bool IsSceneActive();
    }
}
