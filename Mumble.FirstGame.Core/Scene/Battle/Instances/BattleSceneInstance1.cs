using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.Trigger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle.Instances
{
    public class BattleSceneInstance1 : ISceneInstance
    {
        private SpawnSlimeAction _firstSlimeAction;
        public BattleSceneInstance1()
        {
            _firstSlimeAction = new SpawnSlimeAction(3, 30, new PositionComponent(25, 25));
        }
        public List<IAction> GetInitialActions()
        {
            return new List<IAction>(){
                _firstSlimeAction
            };
        }

        public List<ITrigger> GetTriggers()
        {
            return new List<ITrigger>()
            {
                new BattleSceneInstance1Trigger((Slime)_firstSlimeAction.Entity)
            };
        } 
    }
    public class BattleSceneInstance1Trigger : ITrigger
    {
        private HashSet<Slime> _slimes;
        private int _nextNumberOfSlimesToSpawn = 2;

        public BattleSceneInstance1Trigger(Slime firstSlime)
        {
            _slimes = new HashSet<Slime>()
            {
                firstSlime
            };
        }

        public bool CanHandleActionResult(IActionResult result)
        {
            if (result is EntityDestroyedActionResult)
            {
                EntityDestroyedActionResult destroyedResult = (EntityDestroyedActionResult)result;
                if (!(destroyedResult.Entity is Slime))
                {
                    return false;
                }
                if (_slimes.Contains((Slime)destroyedResult.Entity))
                {
                    _slimes.Remove((Slime)destroyedResult.Entity);
                    if (_slimes.Count < 1)
                    {
                        
                        return true;
                    }
                    
                }
            }
            return false;
        }

        public List<IAction> HandleActionResult(IActionResult result)
        {
            List<IAction> actions = new List<IAction>();
            for (int i = 0; i < _nextNumberOfSlimesToSpawn; i++)
            {
                SpawnSlimeAction action = new SpawnSlimeAction(3, 30, new PositionComponent(25+(5*i), 25+(5*i)));
                actions.Add(action);
                _slimes.Add((Slime)action.Entity);
            }
            _nextNumberOfSlimesToSpawn++;
            return actions;
        }

        public bool TryHandleAction(IAction action, IOwnerIdentifier owner)
        {
            return false;
        }
    }
}
