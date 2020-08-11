using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.Battle.Instances;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Core.Scene.Trigger;
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
            IBattleSceneInstance instance = new BattleSceneInstance1();
            collisionSystem.SetSceneBoundary(instance.GetSceneBoundary());
            List<ITrigger> triggers = instance.GetTriggers();
            adapters.AddRange(triggers);
            List<IAction> actionList = instance.GetInitialActions();

            IScene scene = new BattleScene(container, adapters,triggers, collisionSystem,instance.GetSceneBoundary());
            
            Dictionary<IOwnerIdentifier, List<IAction>> actions = new Dictionary<IOwnerIdentifier, List<IAction>>()
            {
                { IntOwnerIdentifier.NonPlayerOwned,actionList }
            };
            scene.Update(actions, 0);
            return scene;
        }
    }
}
