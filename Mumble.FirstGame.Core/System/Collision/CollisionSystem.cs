using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.System.Collision
{
    public class CollisionSystem : ICollisionSystem
    {
        private IEntityContainer _entityContainer;
        private List<IEntity> _entities;
        private Dictionary<OccupiedSpace,IEntity> _spaces;
        private Dictionary<IPositionComponent, IEntity> _positions;

        public CollisionSystem(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
            _entities = _entityContainer.Entities;
            BuildSpace(_entities);
        }

        public IEntity GetEntityByPosition(IPositionComponent position)
        {
            return _positions[position];
        }
        public CollisionResult HasCollision(IPositionComponent position,IPositionComponent selfPosition, IOwnerIdentifier ownerIdentifier)
        {
            //Keep in sync
            BuildSpace(_entityContainer.Entities);
            OccupiedSpace spaceToCheck = new OccupiedSpace(position);
            foreach (OccupiedSpace space in _spaces.Keys)
            {
                if (_spaces[space].PositionComponent == selfPosition)
                {
                    continue;
                }
                if (_spaces[space].OwnerIdentifier == ownerIdentifier)
                {
                    continue;
                }
                if (space.HasCollision(spaceToCheck))
                {
                    return new CollisionResult()
                    {
                        HasCollision = true,
                        CollidedEntity = _spaces[space]
                    };
                }
            }
            return new CollisionResult()
            {
                HasCollision = false
            };
        }
        private void BuildSpace(List<IEntity> entities)
        {
            _entities = entities;
            _spaces = new Dictionary<OccupiedSpace, IEntity>();
            _positions = new Dictionary<IPositionComponent, IEntity>();
            foreach(IEntity entity in _entities)
            {
                _spaces.Add(new OccupiedSpace(entity.PositionComponent), entity);
                _positions.Add(entity.PositionComponent, entity);
            }
        }
        private class OccupiedSpace
        {

            private const float X_FUZZ = 1f;
            private const float Y_FUZZ = 1f;
            public float XMin;
            public float YMin;
            public float XMax;
            public float YMax;
            
            public OccupiedSpace(IPositionComponent position)
            {
                XMin = position.X - X_FUZZ;
                XMax = position.X + X_FUZZ;
                YMin = position.Y - Y_FUZZ;
                YMax = position.Y + Y_FUZZ;
            }
            public bool HasCollision(OccupiedSpace otherSpace)
            {
                if ((otherSpace.XMin > XMax) || (otherSpace.XMax < XMin))
                {
                    return false;
                }
                else if ((otherSpace.YMin > YMax) || (otherSpace.YMax < YMin))
                {
                    return false;
                }

                return true;
            }
        }
    }

}
