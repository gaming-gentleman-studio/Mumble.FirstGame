using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Mumble.FirstGame.Core.Action;

namespace Mumble.FirstGame.Core.Action.Attack
{
    public class AttackAction : IAttackAction
    {
        private ICombatEntity _source;
        private ICombatEntity _target;
        public ActionResult Result { get; set; }

        public AttackAction(ICombatEntity source, ICombatEntity target)
        {
            _source = source;
            _target = target;
        }

        

        public void CalculateEffect()
        {
            _target.HealthComponent.Hit(
                _source.DamageComponent.GetRawDamage());
           if (!_target.HealthComponent.IsAlive())
            {
                Result = new ActionResult(TagId.enemy_killed, _target.GetName());
            }
            else
            {
                Result = new ActionResult(TagId.damage_taken, _target.GetName(), _target.DamageComponent.GetRawDamage());
            }
        }

        public bool EndsEntityTurn()
        {
            return true;
        }

        public bool HasResult()
        {
            return (Result != null);
        }
    }
}
