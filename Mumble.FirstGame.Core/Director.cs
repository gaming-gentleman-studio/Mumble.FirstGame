using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Core.Scene.Factory;
using Mumble.FirstGame.Core.System.Collision;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core
{
    public class Director
    {
        private ISceneFactory _battleSceneFactory;

        public ICollisionSystem CollisionSystem;
        public IEntityContainer EntityContainer { get; private set; }
        public IScene CurrentScene { get; private set; }

        public Director()
        {
            RegisterStartingServices();
            SetCurrentScene();
        }
        private void RegisterStartingServices()
        {
            _battleSceneFactory = new BattleSceneFactory();
            EntityContainer = new BattleEntityContainer();
            CollisionSystem = new CollisionSystem(EntityContainer);
        }
        public void SetCurrentScene()
        {
            CurrentScene = _battleSceneFactory.Create(this, new List<IActionAdapter>());
        }
        public void TransitionScene()
        {

        }
    }
}
