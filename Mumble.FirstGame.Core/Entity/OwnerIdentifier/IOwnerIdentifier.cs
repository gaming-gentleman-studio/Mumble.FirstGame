﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.OwnerIdentifier
{
    public interface IOwnerIdentifier : IEquatable<IOwnerIdentifier>
    {
        bool PlayerOwned();
    }
}
