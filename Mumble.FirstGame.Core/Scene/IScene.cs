using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene
{
    public interface IScene
    {
        IEntityContainer EntityContainer { get; }
        SceneBoundary Boundary { get; }
        List<IActionResult> Update(List<IAction> actions,int elapsedTicks);
        bool IsSceneActive();
    }
}
