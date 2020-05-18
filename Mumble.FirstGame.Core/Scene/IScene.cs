using Mumble.FirstGame.Core.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene
{
    public interface IScene
    {
        List<IAction> Update(List<IAction> actions, TimeSpan elapsed);
        bool IsSceneActive();
    }
}
