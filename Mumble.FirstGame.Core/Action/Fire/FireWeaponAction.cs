using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Components.Weapon;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Fire
{
    public class FireWeaponAction : IFireWeaponAction
    {
        public Direction Direction { get; private set; }
        public ICombatEntity Entity { get; private set; }


        public List<IActionResult> Results
        {
            get;
            private set;
        }

        
        public FireWeaponAction(ICombatEntity sourceEntity, Direction direction)
        {
            Entity = sourceEntity;
            Direction = direction;
            Results = new List<IActionResult>();
        }
        public List<IAction> CalculateEffect(int elapsedTicks)
        {
            List<IAction> actions = new List<IAction>();
            if (Entity.WeaponComponent.AbleToAttack())
            {
                if (Entity.WeaponComponent is IRangedWeaponComponent)
                {
                    IRangedWeaponComponent weapon = (IRangedWeaponComponent)Entity.WeaponComponent;
                    actions.Add(weapon.Attack(Entity.PositionComponent.X, Entity.PositionComponent.Y, Direction, Entity.OwnerIdentifier));
                }
                
            }
            return actions;
        }
        public bool HasResult()
        {
            throw new NotImplementedException();
        }
    }
}
