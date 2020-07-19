using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Core.System.Collision;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Factory
{
    public class BattleSceneFactory : ISceneFactory
    {
        public IScene Create(IEntityContainer container, List<IActionAdapter> adapters, ICollisionSystem collisionSystem)
        {
            IScene scene = new BattleScene(container, adapters, collisionSystem);
            SpawnSlimeAction action = new SpawnSlimeAction(3, 30, new PositionComponent(25, 25));
            Dictionary<IOwnerIdentifier, List<IAction>> actions = new Dictionary<IOwnerIdentifier, List<IAction>>()
            {
                { IntOwnerIdentifier.NonPlayerOwned,new List<IAction>(){ action } }
            };
            scene.Update(actions, 0);
            return scene;
        }
    }
}
