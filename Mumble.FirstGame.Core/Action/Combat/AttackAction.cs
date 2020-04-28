using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Combat
{
    public class AttackAction : ICombatAction
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
                Result = new ActionResult("enemy_killed", _target.GetName());
            }
            else
            {
                Result = new ActionResult("damage_taken", _target.GetName(), _target.DamageComponent.GetRawDamage());
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
