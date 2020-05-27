using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Projectile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.EntityContainer
{
    public class BattleEntityContainer : IEntityContainer
    {
        public List<IMoveableCombatEntity> PlayerTeam => _entities.Values.Where(x => x is IMoveableCombatEntity).Cast<IMoveableCombatEntity>().ToList();
        public List<ICombatAIEntity> EnemyTeam => _entities.Values.Where(x => x is ICombatAIEntity).Cast<ICombatAIEntity>().ToList();


        public List<IProjectileEntity> Projectiles => _entities.Values.Where(x => x is IProjectileEntity).Cast<IProjectileEntity>().ToList();

        private Dictionary<int, IEntity> _entities;
        private int _idx = 0;

        

        public BattleEntityContainer(List<IMoveableCombatEntity> players)
        {
            _entities = new Dictionary<int, IEntity>();
            foreach (IMoveableCombatEntity player in players)
            {
                _entities[_idx] = player;
                _idx++;
            }
        }
        public int AddEntity(IEntity entity)
        {
            _idx++;
            _entities[_idx] = entity;
            return _idx;
        }
        public void AddEntity(int idx, IEntity entity)
        {
            _idx = idx;
            _entities[idx] = entity;
        }
        public IEntity GetEntity(int idx)
        {
            return _entities[idx];
        }
        public void RemoveEntities(HashSet<IEntity> entitiesToRemove)
        {
            if (entitiesToRemove.Count > 0)
            {
                _entities = _entities.Where(x => entitiesToRemove.Contains(x.Value)).ToDictionary(x => x.Key, x => x.Value);
            }
        }
        public void AddEntities(HashSet<IEntity> entitiesToAdd)
        {
            foreach(IEntity entity in entitiesToAdd)
            {
                _idx++;
                _entities[_idx] = entity;
            }
        }

        public int GetEntityId(IEntity entity)
        {
            return _entities.Where(x => x.Value == entity).FirstOrDefault().Key;
        }


    }
}
