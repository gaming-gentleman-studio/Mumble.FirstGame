using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.Components.Weapon;

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
            if (_source.WeaponComponent.AbleToAttack())
            {
                if (_source.WeaponComponent is IMeleeWeaponComponent)
                {
                    IMeleeWeaponComponent weapon = (IMeleeWeaponComponent)_source.WeaponComponent;
                    Results.Add(weapon.Attack(_target));
                    Results.Add(new AttackActionResult(_source, _target));
                }
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
