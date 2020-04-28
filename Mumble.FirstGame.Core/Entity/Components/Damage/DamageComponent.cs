using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Damage
{
    public class DamageComponent : IDamageComponent
    {
        private int _value;
        public DamageComponent(int value)
        {
            _value = value;
        }

        

        public string GetDisplayValue()
        {
            return _value.ToString();
        }

        public int GetRawDamage()
        {
            return _value;
        }
    }
}
