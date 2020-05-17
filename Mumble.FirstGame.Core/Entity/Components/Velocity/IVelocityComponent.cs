using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Velocity
{
    public interface IVelocityComponent : IEntityComponent
    {
        float Speed { get; }
        
        Direction Direction { get; }
    }
}
