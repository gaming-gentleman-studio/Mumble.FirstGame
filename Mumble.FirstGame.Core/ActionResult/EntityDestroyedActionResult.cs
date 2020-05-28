using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class EntityDestroyedActionResult : IActionResult
    {
        public readonly IEntity Entity;
        public EntityDestroyedActionResult(IEntity entity)
        {
            Entity = entity;
        }
    }
}
