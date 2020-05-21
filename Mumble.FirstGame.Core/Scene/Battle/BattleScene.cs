using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle
{

    public class BattleScene : IScene
    {
        public List<IMoveableCombatEntity> PlayerTeam { get; set; }
        public List<ICombatAIEntity> EnemyTeam { get; set; }

        public List<IProjectileEntity> Projectiles { get; set; }
        public SceneBoundary Boundary { get; private set; }

        private TimeSpan _elapsed;

        private int _entityTurn = 0;
        
        public BattleScene(List<IMoveableCombatEntity> playerTeam, List<ICombatAIEntity> enemyTeam, SceneBoundary boundary)
        {
            PlayerTeam = playerTeam;
            EnemyTeam = enemyTeam;
            Projectiles = new List<IProjectileEntity>();
            Boundary = boundary;
        }
        public List<IActionResult> Update(List<IAction> actions, TimeSpan elapsed)
        {
            _elapsed = elapsed;
            List<IAction> resultingActions = new List<IAction>();
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
            
            return resultingActions.Select(x => x.Result).ToList<IActionResult>();
        }
        private List<IAction> ApplyVelocity()
        {
            List<IAction> resultingActions = new List<IAction>();
            HashSet<IProjectileEntity> projectilesToRemove = new HashSet<IProjectileEntity>();
            foreach( IProjectileEntity projectile in Projectiles)
            {
                MoveAction move = new MoveAction(projectile, projectile.VelocityComponent);
                move.CalculateEffect(Boundary);
                if (((MoveActionResult)move.Result).OutOfBounds == true)
                {
                    projectilesToRemove.Add(projectile);
                }
                resultingActions.Add(move);
            }
            Projectiles = Projectiles.Where(x => !projectilesToRemove.Contains(x)).ToList();
            return resultingActions;
        }
        private List<IAction> Update(IFireWeaponAction fireAction)
        {
            Projectiles.AddRange(fireAction.CalculateEffect(_elapsed));
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
                    foreach(ICombatAIEntity enemy in EnemyTeam)
                    {
                        //PlayerTeam.ToList inneficient?
                        IAttackAction enemyAction = enemy.CombatAIComponent.GenerateCombatAction(enemy, PlayerTeam.ToList<ICombatEntity>());
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
            return _entityTurn > PlayerTeam.Count()-1;
        }

        public bool IsSceneActive()
        {
            return (EnemyTeam.Count(enemy => enemy.HealthComponent.IsAlive()) > 0);
        }
    }
}
