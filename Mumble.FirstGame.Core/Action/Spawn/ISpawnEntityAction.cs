using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Spawn
{
    public interface ISpawnEntityAction : IAction
    {
        IEntity Entity { get; }
        void CalculateEffect(IEntityContainer container);
    }
}
