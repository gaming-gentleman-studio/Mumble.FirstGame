using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
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
        public BattleEntityContainer EntityContainer { get; private set; }
        public SceneBoundary Boundary { get; private set; }

        private int _elapsedTicks;

        private int _entityTurn = 0;
        
        public BattleScene(BattleEntityContainer entityContainer, SceneBoundary boundary)
        {
            EntityContainer = entityContainer;
            Boundary = boundary;
        }
        public List<IActionResult> Update(List<IAction> actions,int elapsedTicks)
        {
            List<IAction> resultingActions = new List<IAction>();
            _elapsedTicks = elapsedTicks;
            
            resultingActions.AddRange(ApplyVelocity());
            foreach (IAction action in actions)
            {
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
            }
            
            return resultingActions.Select(x => x.Result).Where(x => x != null).ToList<IActionResult>();
        }
        private List<IAction> ApplyVelocity()
        {
            List<IAction> resultingActions = new List<IAction>();
            HashSet<IEntity> projectilesToRemove = new HashSet<IEntity>();
            foreach( IProjectileEntity projectile in EntityContainer.Projectiles)
            {
                MoveAction move = new MoveAction(projectile, projectile.VelocityComponent);
                move.CalculateEffect(Boundary);
                if (((MoveActionResult)move.Result).OutOfBounds == true)
                {
                    projectilesToRemove.Add(projectile);
                }
                resultingActions.Add(move);
            }
            EntityContainer.RemoveEntities(projectilesToRemove);
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
