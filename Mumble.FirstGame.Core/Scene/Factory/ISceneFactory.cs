using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Factory
{
    public interface ISceneFactory
    {
        IScene Create(IEntityContainer container, List<IActionAdapter> adapters);
    }
}
