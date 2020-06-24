using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.EntityContainer
{
    public interface IEntityContainer
    {
        List<IMoveableCombatEntity> PlayerTeam { get; }
        List<ICombatAIEntity> EnemyTeam { get; }
        List<IEntity> Entities { get; }
        List<IProjectileEntity> Projectiles { get; }
        int AddEntity(IEntity entity);
        void AddEntity(int idx, IEntity entity);

        IEntity GetEntity(int idx, bool includeSoftDeleted=false);
        bool HasEntity(int idx, bool includeSoftDeleted = false);
        int GetEntityId(IEntity entity);
        void RemoveEntities(HashSet<IEntity> entitiesToRemove);
        void AddEntities(HashSet<IEntity> entitiesToAdd);
        List<IEntity> GetSoftDeletedEntities();
        void HardDeleteEntities();
    }
}
