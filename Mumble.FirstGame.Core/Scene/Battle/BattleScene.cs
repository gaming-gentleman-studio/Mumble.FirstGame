using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Battle
{

    public class BattleScene : IScene
    {
        public List<ICombatEntity> PlayerTeam { get; set; }
        public List<ICombatAIEntity> EnemyTeam { get; set; }

        private int _entityTurn = 0;
        
        public BattleScene(List<ICombatEntity> playerTeam, List<ICombatAIEntity> enemyTeam)
        {
            PlayerTeam = playerTeam;
            EnemyTeam = enemyTeam;
        }
        public List<IAction> Update(IAction action)
        {
            
            if (!(action is IAttackAction)){
                return new List<IAction>();
            }
            IAttackAction combatAction = (IAttackAction) action;
            return Update(combatAction);
        }
        private List<IAction> Update(IAttackAction action)
        {
            List<IAction> results = new List<IAction>();
            results.Add(action);
            action.CalculateEffect();
            if (action.EndsEntityTurn())
            {
                _entityTurn++;
                if (isEnemyTurn())
                {
                    foreach(ICombatAIEntity enemy in EnemyTeam)
                    {
                        IAttackAction enemyAction = enemy.CombatAIComponent.GenerateCombatAction(enemy, PlayerTeam);
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
