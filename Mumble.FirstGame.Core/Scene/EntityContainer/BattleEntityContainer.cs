using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.EntityContainer
{
    public class BattleEntityContainer : IEntityContainer
    {
        public List<IMoveableCombatEntity> PlayerTeam => _entities.Values.Where(x => x.Entity is IMoveableCombatEntity && x.SoftDeleted == false).Select(x => x.Entity).Cast<IMoveableCombatEntity>().ToList();
        public List<ICombatAIEntity> EnemyTeam => _entities.Values.Where(x => x.Entity is ICombatAIEntity && x.SoftDeleted == false).Select(x => x.Entity).Cast<ICombatAIEntity>().ToList();


        public List<IProjectileEntity> Projectiles => _entities.Values.Where(x => (x.Entity is IProjectileEntity && x.SoftDeleted == false)).Select(x => x.Entity).Cast<IProjectileEntity>().ToList();

        public List<IEntity> Entities => _entities.Values.Where(x => (x.SoftDeleted == false)).Select(x => x.Entity).ToList();
        private ConcurrentDictionary<int, EntityMeta> _entities;
        private int _idx = 0;

        private class EntityMeta
        {
            public IEntity Entity;
            public bool SoftDeleted { get; private set;}
            private int _softDeletePersistanceCyclesLeft = 5;
            public EntityMeta(IEntity entity)
            {
                Entity = entity;
                SoftDeleted = false;
            }
            public bool CanHardDelete()
            {
                _softDeletePersistanceCyclesLeft--;
                return (_softDeletePersistanceCyclesLeft <= 0);
            }
            public void SoftDelete()
            {
                SoftDeleted = true;
            }
            
        }
        public BattleEntityContainer()
        {
            _entities = new ConcurrentDictionary<int, EntityMeta>();
        }
        public BattleEntityContainer(List<IMoveableCombatEntity> players)
        {
            _entities = new ConcurrentDictionary<int, EntityMeta>();
            foreach (IMoveableCombatEntity player in players)
            {
                _entities[_idx] = new EntityMeta(player);
                _idx++;
            }
        }
        public int AddEntity(IEntity entity)
        {
            _idx++;
            _entities[_idx] = new EntityMeta(entity);
            return _idx;
        }
        public void AddEntity(int idx, IEntity entity)
        {
            _idx = idx;
            if (!_entities.ContainsKey(_idx))
            {
                _entities[idx] = new EntityMeta(entity);
            }
            
        }
        public IEntity GetEntity(int idx, bool includeSoftDeleted = false)
        {
            EntityMeta meta = _entities[idx];
            if (meta.SoftDeleted && !includeSoftDeleted)
            {
                throw new Exception("Non deleted instance of entity not found in container");
            }
            return meta.Entity;
        }
        public int GetEntityId(IEntity entity)
        {
            return _entities.Where(x => x.Value.Entity == entity).FirstOrDefault().Key;
        }
        public void RemoveEntities(HashSet<IEntity> entitiesToRemove)
        {
            if (entitiesToRemove.Count > 0)
            {
                List<EntityMeta> metas = _entities.Values.Where(x => entitiesToRemove.Contains(x.Entity)).ToList();
                foreach(EntityMeta meta in metas)
                {
                    meta.SoftDelete();
                }
            }
        }
        public void AddEntities(HashSet<IEntity> entitiesToAdd)
        {
            foreach(IEntity entity in entitiesToAdd)
            {
                AddEntity(entity);
            }
        }



        public void HardDeleteEntities()
        {
            HashSet<int> entitiesToRemove = new HashSet<int>();
            foreach(int idx in _entities.Keys)
            {
                EntityMeta meta = _entities[idx];
                if (meta.SoftDeleted)
                {
                    if (meta.CanHardDelete())
                    {
                        entitiesToRemove.Add(idx);
                    }
                }
            }
            foreach (int idx in entitiesToRemove)
            {
                _entities.TryRemove(idx,out _);
            }
        }

        public List<IEntity> GetSoftDeletedEntities()
        {
            return _entities.Values.Where(x => x.SoftDeleted == true).Select(x => x.Entity).ToList();
        }

        public bool HasEntity(int idx, bool includeSoftDeleted = false)
        {
            if (_entities.ContainsKey(idx))
            {
                EntityMeta meta = _entities[idx];
                if (meta.SoftDeleted && !includeSoftDeleted)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
