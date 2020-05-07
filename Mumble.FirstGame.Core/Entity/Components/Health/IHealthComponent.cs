using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Health
{
    public interface IHealthComponent : IEntityComponent
    {
        void Hit(int damage);
        void Heal(int restored);
        void SetMax(int max);
        bool IsAlive();

        int GetCurrentHealth();
        int GetMaxHealth();
    }
}
