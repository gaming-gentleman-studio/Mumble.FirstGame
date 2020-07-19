using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Core.System.Collision;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Factory
{
    public interface ISceneFactory
    {
        IScene Create(IEntityContainer container, List<IActionAdapter> adapters,ICollisionSystem collisionSystem);
    }
}
