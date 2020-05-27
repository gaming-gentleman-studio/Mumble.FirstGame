using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.EntityContainer
{
    public interface IEntityContainer
    {
        int AddEntity(IEntity entity);
        void AddEntity(int idx, IEntity entity);

        IEntity GetEntity(int idx);

        int GetEntityId(IEntity entity);
        void RemoveEntities(HashSet<IEntity> entitiesToRemove);
        void AddEntities(HashSet<IEntity> entitiesToAdd);
    }
}
