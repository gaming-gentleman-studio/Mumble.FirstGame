using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle
{

    public class BattleScene : IScene
    {
        public IEntityContainer EntityContainer { get; private set; }
        private List<IActionAdapter> _actionAdapters;
        public SceneBoundary Boundary { get; private set; }

        private int _elapsedTicks;

        private int _entityTurn = 0;
        
        public BattleScene(IEntityContainer container,List<IActionAdapter> actionAdapters)
        {
            EntityContainer = container;
            Boundary = new SceneBoundary(100, 100);
            _actionAdapters = actionAdapters;
        }
        public List<IActionResult> Update(Dictionary<IOwnerIdentifier, List<IAction>> actions, int elapsedTicks)
        {
            EntityContainer.HardDeleteEntities();
            List<IAction> resultingActions = new List<IAction>();
            _elapsedTicks = elapsedTicks;
            
            for(int i = 0; i<elapsedTicks; i++)
            {
                resultingActions.AddRange(ApplyVelocity());
            }
            
            foreach(IOwnerIdentifier owner in actions.Keys)
            {
                foreach (IAction action in actions[owner])
                {
                    foreach(IActionAdapter adapter in _actionAdapters)
                    {
                        if (adapter.TryHandleAction(action, owner))
                        {
                            resultingActions.Add(action);
                        }
                        
                    }
                    if (action is IMoveAction)
                    {
                        IMoveAction moveAction = (IMoveAction)action;
                        resultingActions.AddRange(Update(moveAction));
                    }
                    else if (action is IAttackAction)
                    {
                        IAttackAction combatAction = (IAttackAction)action;
                        resultingActions.AddRange(Update(combatAction));
                    }
                    else if (action is IFireWeaponAction)
                    {
                        IFireWeaponAction fireAction = (IFireWeaponAction)action;
                        resultingActions.AddRange(Update(fireAction));
                    }
                    else if (action is ISpawnEntityAction)
                    {
                        ISpawnEntityAction spawnAction = (ISpawnEntityAction)action;
                        resultingActions.AddRange(Update(spawnAction));
                    }
                }
            }

            
            List<IActionResult> results = new List<IActionResult>();
            HashSet<IEntity> entitiesToRemove = new HashSet<IEntity>();
            foreach(IAction action in resultingActions)
            {
                SingleEntityCleanup(action, entitiesToRemove);
                results.AddRange(action.Results);
                
            }
            EntityContainer.RemoveEntities(entitiesToRemove);
            return results;
        }
        private void SingleEntityCleanup(IAction action, HashSet<IEntity> entitiesToRemove)
        {
            foreach (IActionResult result in action.Results)
            {
                if (result is EntityDestroyedActionResult)
                {
                    EntityDestroyedActionResult destroyedResult = (EntityDestroyedActionResult)result;
                    entitiesToRemove.Add(destroyedResult.Entity);
                }
            }
        }
        private List<IActionResult> RegenerateEntityDestroyedResults()
        {
            List<IEntity> entities = EntityContainer.GetSoftDeletedEntities();
            List<IActionResult> results = new List<IActionResult>();
            foreach(IEntity entity in entities)
            {
                results.Add(new EntityDestroyedActionResult(entity));
            }
            return results;
        }
        private List<IAction> ApplyVelocity()
        {
            List<IAction> resultingActions = new List<IAction>();
            foreach( IProjectileEntity projectile in EntityContainer.Projectiles)
            {
                MoveAction move = new MoveAction(projectile, projectile.VelocityComponent);
                move.CalculateEffect(Boundary);
                resultingActions.Add(move);
            }
            
            return resultingActions;
        }
        private List<IAction> Update(IFireWeaponAction fireAction)
        {
            EntityContainer.AddEntities(new HashSet<IEntity>(fireAction.CalculateEffect(_elapsedTicks)));
            return new List<IAction>() { fireAction };
        }
        private List<IAction> Update(IMoveAction moveAction)
        {
            moveAction.CalculateEffect(Boundary);

            //TODO - enemies may also move
            return new List<IAction>() { moveAction };
        }
        private List<IAction> Update(IAttackAction combatAction)
        {
            List<IAction> results = new List<IAction>();
            results.Add(combatAction);
            combatAction.CalculateEffect();
            if (combatAction.EndsEntityTurn())
            {
                _entityTurn++;
                if (isEnemyTurn())
                {
                    foreach(ICombatAIEntity enemy in EntityContainer.EnemyTeam)
                    {
                        //PlayerTeam.ToList inneficient?
                        IAttackAction enemyAction = enemy.CombatAIComponent.GenerateCombatAction(enemy, EntityContainer.PlayerTeam.ToList<ICombatEntity>());
                        enemyAction.CalculateEffect();
                        results.Add(enemyAction);
                    }
                    _entityTurn = 0;
                }
                
            }
            return results;
        }
        private List<IAction> Update(ISpawnEntityAction spawnAction)
        {
            spawnAction.CalculateEffect(EntityContainer);
            return new List<IAction>() { spawnAction };
        }
        private bool isEnemyTurn()
        {
            return _entityTurn > EntityContainer.PlayerTeam.Count()-1;
        }

        public bool IsSceneActive()
        {
            return (EntityContainer.EnemyTeam.Count(enemy => enemy.HealthComponent.IsAlive()) > 0);
        }
    }
}
