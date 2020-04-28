using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Player
{
    public class Player : ICombatEntity
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

        public Tag HealthTag => new Tag("hp_display", GetName(), HealthComponent.GetCurrentHealth(), HealthComponent.GetMaxHealth());

        public Tag DamageTag => new Tag("damage_display", GetName(), DamageComponent.GetRawDamage());

        public Player(int damage, int health)
        {
            DamageComponent = new DamageComponent(damage);
            HealthComponent = new HealthComponent(health);
        }

        public string GetName()
        {
            return "Player";
        }
    }
}
