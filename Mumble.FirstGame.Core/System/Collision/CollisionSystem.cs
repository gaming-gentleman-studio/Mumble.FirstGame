using Mumble.FirstGame.Core.Background;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Size;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Scene.Battle.SceneBoundary;
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
        private Dictionary<OccupiedSpace, IBackground> _backgrounds;
        private Dictionary<IPositionComponent, IEntity> _positions;
        private ISceneBoundary _boundary;

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
        public CollisionResult HasCollision(IPositionComponent position,IPositionComponent selfPosition, ISizeComponent selfSize,IOwnerIdentifier ownerIdentifier)
        {
            //Keep in sync
            BuildSpace(_entityContainer.Entities);
            OccupiedSpace spaceToCheck = new OccupiedSpace(position, selfSize);
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
            foreach(OccupiedSpace space in _backgrounds.Keys)
            {
                if (space.HasCollision(spaceToCheck))
                {
                    return new CollisionResult()
                    {
                        HasCollision = true,
                        InBounds = false,
                        BouncebackPosition = GetBouncebackPosition(space,position,selfPosition, selfSize)
                    };
                }
            }
            return new CollisionResult()
            {
                HasCollision = false
            };
        }

        public void SetSceneBoundary(ISceneBoundary boundary)
        {
            _boundary = boundary;
            BuildSpace(_entityContainer.Entities);
        }
        //TODO - this will require a lot of thought I think
        private IPositionComponent GetBouncebackPosition(OccupiedSpace target, IPositionComponent targetPosition, IPositionComponent selfPosition, ISizeComponent size)
        {
            return selfPosition;

        }
        private void BuildSpace(List<IEntity> entities)
        {
            _entities = entities;
            _spaces = new Dictionary<OccupiedSpace, IEntity>();
            _positions = new Dictionary<IPositionComponent, IEntity>();
            _backgrounds = new Dictionary<OccupiedSpace, IBackground>();
            foreach(IEntity entity in _entities)
            {
                _spaces.Add(new OccupiedSpace(entity.PositionComponent,entity.SizeComponent), entity);
                _positions.Add(entity.PositionComponent, entity);
            }
            if (_boundary != null)
            {
                for (int i = 0; i < _boundary.Backgrounds.GetLength(0); i++)
                {
                    for (int j = 0; j < _boundary.Backgrounds.GetLength(1); j++)
                    {
                        IBackground background = _boundary.Backgrounds[i, j];
                        if (background.HasCollision)
                        {
                            _backgrounds.Add(new OccupiedSpace((float)i, (float)j, background.Scale), background);
                        }
                    }
                }
            }

        }
        private class OccupiedSpace
        {

            private const float X_FUZZ = 0.5f;
            private const float Y_FUZZ = 0.5f;
            public float XMin;
            public float YMin;
            public float XMax;
            public float YMax;
            
            public OccupiedSpace(IPositionComponent position, ISizeComponent size)
            {
                XMin = position.X - (X_FUZZ*size.Scale);
                XMax = position.X + (X_FUZZ * size.Scale);
                YMin = position.Y - (Y_FUZZ * size.Scale);
                YMax = position.Y + (Y_FUZZ * size.Scale);
            }
            public OccupiedSpace(float x, float y, float size)
            {
                XMin = x - (X_FUZZ * size);
                XMax = x + (X_FUZZ * size);
                YMin = y - (Y_FUZZ * size);
                YMax = y + (Y_FUZZ * size);
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
