using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;

namespace Mumble.FirstGame.Core.Action.Attack
{
    public class AttackAction : IAttackAction
    {
        private ICombatEntity _source;
        private ICombatEntity _target;
        public List<IActionResult> Results { get; set; }

        public AttackAction(ICombatEntity source, ICombatEntity target)
        {
            _source = source;
            _target = target;
            Results = new List<IActionResult>();
        }

        

        public void CalculateEffect()
        {
            _target.HealthComponent.Hit(
                _source.WeaponComponent.DamageComponent.GetRawDamage());
           if (!_target.HealthComponent.IsAlive())
            {
                Results.Add(new EntityDestroyedActionResult(_target));
            }
            else
            {
                Results.Add(new DamageActionResult(_target, _target.WeaponComponent.DamageComponent.GetRawDamage()));
            }
        }

        public bool EndsEntityTurn()
        {
            return true;
        }

        public bool HasResult()
        {
            return (Results.Count > 0);
        }
    }
}
