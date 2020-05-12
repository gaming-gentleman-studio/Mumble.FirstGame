using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Entity.Components.AI;
using Mumble.FirstGame.Core.Entity.Components.Damage;
using Mumble.FirstGame.Core.Entity.Components.Health;
using Mumble.FirstGame.Core.TagArguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Enemy
{
    public class Slime : ICombatAIEntity
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
        public ICombatAIComponent CombatAIComponent
        {
            get;
            private set;
        }

        public Tag HealthTag => new Tag(TagId.hp_display, new HPArguments(GetName(), HealthComponent.GetCurrentHealth(), HealthComponent.GetMaxHealth()));

        public Tag DamageTag => new Tag(TagId.damage_display, new DamageArguments(GetName(), DamageComponent.GetRawDamage()));

        public Slime(int damage, int health)
        {
            DamageComponent = new DamageComponent(damage);
            HealthComponent = new HealthComponent(health);
            CombatAIComponent = new SimpleAttackAIComponent();
        }

        public string GetName()
        {
            return "Slime";
        }
    }
}
