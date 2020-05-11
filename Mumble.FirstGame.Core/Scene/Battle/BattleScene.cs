using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity;
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

        public SceneBoundary Boundary { get; private set; }

        private int _entityTurn = 0;
        
        public BattleScene(List<IMoveableCombatEntity> playerTeam, List<ICombatAIEntity> enemyTeam, SceneBoundary boundary)
        {
            PlayerTeam = playerTeam;
            EnemyTeam = enemyTeam;
            Boundary = boundary;
        }
        public List<IAction> Update(IAction action)
        {
            if (action is IMoveAction)
            {
                IMoveAction moveAction = (IMoveAction)action;
                return Update(moveAction);
            }
            if (action is IAttackAction){
                IAttackAction combatAction = (IAttackAction)action;
                return Update(combatAction); 
            }
            return new List<IAction>();
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
