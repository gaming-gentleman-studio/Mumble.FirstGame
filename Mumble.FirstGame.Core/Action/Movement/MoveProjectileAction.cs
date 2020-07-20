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
    public class MoveProjectileAction : IMoveAction
    {
        public IVelocityComponent Velocity { get; private set; }
        public List<IActionResult> Results { get; private set; }
        public IMoveableEntity Entity { get; private set; }

        //Use when entity velocity has no direction
        public MoveProjectileAction(IProjectileEntity entity, Direction direction)
        {
            Entity = entity;
            Velocity = new VelocityComponent(direction, Entity.VelocityComponent.Speed);
            Results = new List<IActionResult>();

        }
        //DO NOT USE IN FRONT END Use when entity velocity already has direction

        // TODO - how to make internal for shared projects?
        public MoveProjectileAction(IProjectileEntity entity, IVelocityComponent velocity)
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
                if (!collisionSystem.HasCollision(newPosition, Entity.PositionComponent,Entity.OwnerIdentifier))
                {
                    Entity.PositionComponent.Move(newPosition);
                    Results.Add(new MoveActionResult(Entity, newPosition.X, newPosition.Y));
                }
                else
                {
                    Results.Add(new EntityDestroyedActionResult(Entity));
                }
            }
            else
            {
                Results.Add(new EntityDestroyedActionResult(Entity));
            }

        }
    }
}
