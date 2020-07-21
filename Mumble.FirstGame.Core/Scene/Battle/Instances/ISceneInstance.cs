using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Scene.Trigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle.Instances
{
    public interface ISceneInstance
    {
        List<IAction> GetInitialActions();
        List<ITrigger> GetTriggers();
    }
}
