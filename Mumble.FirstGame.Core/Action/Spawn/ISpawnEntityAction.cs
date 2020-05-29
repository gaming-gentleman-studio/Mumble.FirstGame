using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Spawn
{
    public interface ISpawnEntityAction : IAction
    {
        IEntity Entity { get; }
        IEntity CalculateEffect();
    }
}
