using Mumble.FirstGame.Core.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene
{
    public interface IScene
    {
        public List<IAction> Update(IAction action);
        public bool IsSceneActive();
    }
}
