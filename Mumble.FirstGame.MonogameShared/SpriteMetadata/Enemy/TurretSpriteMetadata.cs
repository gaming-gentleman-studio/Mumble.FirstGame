using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.SpriteMetadata.Enemy
{
    public class TurretSpriteMetadata : BaseEnemySpriteMetadata
    {
        protected override IEntity _entity { get; }
        public TurretSpriteMetadata(IEntity entity)
        {
            _entity = entity;
        }
        public override Texture2D GetImage(ContentImages container)
        {
            return container.Turret;
        }
    }
}
