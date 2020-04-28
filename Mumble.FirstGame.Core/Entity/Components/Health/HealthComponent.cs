using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Health
{
    public class HealthComponent : IHealthComponent
    {
        private int _maxHealth;
        private int _currentHealth;
        

        public HealthComponent(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }

        public int GetMaxHealth()
        {
            return _maxHealth;
        }

        public void Heal(int restored)
        {
            _currentHealth += restored;
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }

        public void Hit(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
        }

        public bool IsAlive()
        {
            return (_currentHealth > 0);
        }

        public void SetMax(int max)
        {
            throw new NotImplementedException();
        }
    }
}
