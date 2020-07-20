using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Entity.Projectile;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.System.Collision;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Movement
{
    public class MoveAction : IMoveAction
    {

        public IVelocityComponent Velocity { get; private set; }
        public List<IActionResult> Results { get; private set; }
        public IMoveableEntity Entity { get; private set; }

        //Use when entity velocity has no direction
        public MoveAction(IMoveableEntity entity, Direction direction)
        {
            Entity = entity;
            Velocity = new VelocityComponent(direction, Entity.VelocityComponent.Speed);
            Results = new List<IActionResult>();
            
        }
        //DO NOT USE IN FRONT END Use when entity velocity already has direction

        // TODO - how to make internal for shared projects?
        public MoveAction(IMoveableEntity entity, IVelocityComponent velocity)
        {
            Entity = entity;
            Velocity = velocity;
            Results = new List<IActionResult>();

        }
        public bool HasResult()
        {
            return true;
        }

        public void CalculateEffect(SceneBoundary boundary, ICollisionSystem collisionSystem)
        {
            IPositionComponent newPosition = Entity.PositionComponent.GetNewCoords(Velocity);
            if (boundary.IsInBounds(newPosition))
            {
                if (!collisionSystem.HasCollision(newPosition,Entity.PositionComponent,Entity.OwnerIdentifier).HasCollision)
                {
                    Entity.PositionComponent.Move(newPosition);
                    Results.Add(new MoveActionResult(Entity, newPosition.X, newPosition.Y));
                }
            }
            else
            {
                if (boundary.IsInBoundsX(newPosition))
                {
                    newPosition = new PositionComponent(newPosition.X, boundary.GetBoundsAdjustedY(newPosition));
                }
                if (boundary.IsInBoundsY(newPosition))
                {
                    newPosition = new PositionComponent(boundary.GetBoundsAdjustedX(newPosition), newPosition.Y);
                }
                Entity.PositionComponent.Move(newPosition);
                Results.Add(new MoveActionResult(Entity, Entity.PositionComponent.X, Entity.PositionComponent.Y,true));
            }
            Entity.PositionComponent.ChangeFacing(Velocity.Direction);

        }
    }
}
