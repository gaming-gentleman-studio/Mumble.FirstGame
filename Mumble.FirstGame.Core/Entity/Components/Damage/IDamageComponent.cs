﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Damage
{
    public interface IDamageComponent: IEntityComponent
    {
        public int GetRawDamage();

    }
}
