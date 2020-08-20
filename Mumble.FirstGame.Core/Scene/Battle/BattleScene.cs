using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Meta;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Action.Spawn;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene.Battle.SceneBoundary;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Core.System.Collision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle
{

    public class BattleScene : IScene
    {
        public IEntityContainer EntityContainer => _director.EntityContainer;
        private IEnumerable<IActionAdapter> _actionAdapters;
        private IEnumerable<IActionResultAdapter> _actionResultAdapters;
        private List<IAction> _actionsFromActionResultAdapters;
        private ICollisionSystem _collisionSystem => _director.CollisionSystem;
        private Director _director;
        public ISceneBoundary Boundary { get; private set; }

        private int _elapsedTicks;

        private int _entityTurn = 0;
        
        public BattleScene(Director director,IEnumerable<IActionAdapter> actionAdapters, IEnumerable<IActionResultAdapter> actionResultAdapters, ISceneBoundary boundary)
        {
            _director = director;
            Boundary = boundary;
            _actionAdapters = actionAdapters;
            _actionResultAdapters = actionResultAdapters;
            _actionsFromActionResultAdapters = new List<IAction>();
        }
        public List<IActionResult> Update(Dictionary<IOwnerIdentifier, List<IAction>> actions, int elapsedTicks)
        {
            EntityContainer.HardDeleteEntities();
            List<IAction> resultingActions = new List<IAction>();
            _elapsedTicks = elapsedTicks;
            
            for(int i = 0; i<elapsedTicks; i++)
            {
                resultingActions.AddRange(ApplyVelocity());
                resultingActions.AddRange(ApplyAI());
            }
            ApplyWeaponCooldowns();
            foreach (IAction action in _actionsFromActionResultAdapters)
            {
                DispatchAction(action, IntOwnerIdentifier.NonPlayerOwned, resultingActions);
            }
            _actionsFromActionResultAdapters = new List<IAction>();
            foreach (IOwnerIdentifier owner in actions.Keys)
            {
                foreach (IAction action in actions[owner])
                {
                    DispatchAction(action, owner, resultingActions);
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
            ResultProcessing(results);
            return results;
        }
        private void ResultProcessing(List<IActionResult> results)
        {
            foreach (IActionResult result in results)
            {
                foreach (IActionResultAdapter adapter in _actionResultAdapters)
                {
                    if (adapter.CanHandleActionResult(result))
                    {
                        _actionsFromActionResultAdapters.AddRange(adapter.HandleActionResult(result));
                    }
                }
            }
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
            foreach(IProjectileEntity projectile in EntityContainer.Projectiles)
            {
                MoveProjectileAction move = new MoveProjectileAction(projectile, projectile.VelocityComponent);
                move.CalculateEffect(_collisionSystem);
                resultingActions.Add(move);
            }
            
            return resultingActions;
        }
        private List<IAction> ApplyAI()
        {
            List<IAction> resultingActions = new List<IAction>();
            foreach (ICombatAIEntity enemy in EntityContainer.EnemyTeam)
            {
                //PlayerTeam.ToList inneficient?
                IAction enemyAction = enemy.CombatAIComponent.GenerateAction(enemy, EntityContainer.PlayerTeam.ToList<ICombatEntity>());
                resultingActions = DispatchAction(enemyAction, IntOwnerIdentifier.NonPlayerOwned, resultingActions);
            }
            return resultingActions;
        }
        private void ApplyWeaponCooldowns()
        {
            List<ICombatEntity> entities = EntityContainer.Entities.Where(x => x is ICombatEntity).Cast<ICombatEntity>().ToList();
            foreach (ICombatEntity entity in entities)
            {
                entity.WeaponComponent.ApplyCooldown(_elapsedTicks);
            }
        }

        private List<IAction> DispatchAction(IAction action,IOwnerIdentifier owner,List<IAction> resultingActions)
        {
            foreach (IActionAdapter adapter in _actionAdapters)
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
                resultingActions.AddRange(Update(fireAction,owner));
            }
            else if (action is ISpawnEntityAction)
            {
                ISpawnEntityAction spawnAction = (ISpawnEntityAction)action;
                resultingActions.AddRange(Update(spawnAction));
            }
            else if (action is EnterSceneAction)
            {
                EnterSceneAction enterAction = (EnterSceneAction)action;
                resultingActions.AddRange(Update(enterAction,owner));
            }
            return resultingActions;
        }
        private List<IAction> Update(EnterSceneAction enterAction, IOwnerIdentifier owner)
        {
            SpawnPlayerAction spawnAction = new SpawnPlayerAction("Beau", 3, 10, owner);
            List<IAction> resultingActions = Update(spawnAction);
            enterAction.CalculateEffect();
            resultingActions.Add(enterAction);
            resultingActions.Add(spawnAction);
            return resultingActions;
        }
        private List<IAction> Update(IFireWeaponAction fireAction, IOwnerIdentifier owner)
        {
            List<IAction> actions = fireAction.CalculateEffect(_elapsedTicks);
            List<IAction> resultingActions = new List<IAction>();
            foreach(IAction action in actions)
            {
                resultingActions = DispatchAction(action, owner, resultingActions);
            }
            resultingActions.Add(fireAction);
            return resultingActions;
        }
        private List<IAction> Update(IMoveAction moveAction)
        {
            moveAction.CalculateEffect( _collisionSystem);
            return new List<IAction>() { moveAction };
        }
        private List<IAction> Update(IAttackAction combatAction)
        {
            List<IAction> resultingActions = new List<IAction>();
            resultingActions.Add(combatAction);
            combatAction.CalculateEffect();
            return resultingActions;
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
