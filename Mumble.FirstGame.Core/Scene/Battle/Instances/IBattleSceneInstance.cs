using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Scene.Battle.SceneBoundary;
using Mumble.FirstGame.Core.Scene.Trigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle.Instances
{
    public interface IBattleSceneInstance
    {
        List<IAction> GetInitialActions();
        List<ITrigger> GetTriggers();

        ISceneBoundary GetSceneBoundary();
    }
}
