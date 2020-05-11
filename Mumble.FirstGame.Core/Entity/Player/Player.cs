using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using Mumble.FirstGame.Core.Entity.Components.Position;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Player
{
    public class Player : IMoveableCombatEntity
    {
        public IDamageComponent DamageComponent
        {
            get;
            private set;
        }
        public IHealthComponent HealthComponent
        {
            get;
            private set;
        }

        public IPositionComponent PositionComponent
        {
            get;
            private set;
        }

        public Tag HealthTag => new Tag(TagId.hp_display, GetName(), HealthComponent.GetCurrentHealth(), HealthComponent.GetMaxHealth());

        public Tag DamageTag => new Tag(TagId.damage_display, GetName(), DamageComponent.GetRawDamage());

        

        public Player(int damage, int health)
        {
            DamageComponent = new DamageComponent(damage);
            HealthComponent = new HealthComponent(health);
            PositionComponent = new PositionComponent(0, 0);
        }

        public string GetName()
        {
            return "Player";
        }
    }
}
