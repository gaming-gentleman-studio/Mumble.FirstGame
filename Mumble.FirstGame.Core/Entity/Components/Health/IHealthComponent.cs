using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Health
{
    public interface IHealthComponent : IEntityComponent
    {
        public void Hit(int damage);
        public void Heal(int restored);
        public void SetMax(int max);
        public bool IsAlive();

        public int GetCurrentHealth();
        public int GetMaxHealth();
    }
}
