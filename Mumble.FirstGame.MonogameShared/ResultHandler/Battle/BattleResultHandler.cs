using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Position;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.ResultHandler
{
    public class BattleResultHandler : IResultHandler
    {
        private Player _player;
        private Dictionary<IEntity, AbstractSpriteMetadata> _entitySprites;
        public BattleResultHandler(Player player, Dictionary<IEntity, AbstractSpriteMetadata> entitySprites)
        {
            _player = player;
            _entitySprites = entitySprites;
        }
        public void ApplyResults(List<IActionResult> results)
        {
            foreach (EntitiesCreatedActionResult result in results.Where(x => x is EntitiesCreatedActionResult))
            {
                foreach (IEntity entity in result.Entities)
                {
                    _entitySprites[entity] = SpriteMetadataFactory.CreateSpriteMetadata(entity);
                }

            }
            foreach (MoveActionResult result in results.Where(x => x is MoveActionResult))
            {
                if (!_entitySprites.Keys.Contains(result.Entity))
                {
                    _entitySprites[result.Entity] = SpriteMetadataFactory.CreateSpriteMetadata(result.Entity);
                }
                _entitySprites[result.Entity].AnimateMovement(result);
                if (result.OutOfBounds)
                {
                    if (result.Entity != _player)
                    {
                        _entitySprites.Remove(result.Entity);
                    }

                }
                else
                {
                    //this is a little awkwardly placed I think
                    PositionComponent newPos = new PositionComponent(result.XPos, result.YPos);
                    result.Entity.PositionComponent.Move(newPos);
                }

            }
            foreach (EntityDestroyedActionResult result in results.Where(x => x is EntityDestroyedActionResult))
            {
                _entitySprites.Remove(result.Entity);
            }
            foreach (DamageActionResult result in results.Where(x => x is DamageActionResult))
            {
                _entitySprites[result.Entity].AnimateDamage();
            }
            foreach (AttackActionResult result in results.Where(x => x is AttackActionResult))
            {
                _entitySprites[result.Source].AnimateAttack();
            }
        }
    }
}
