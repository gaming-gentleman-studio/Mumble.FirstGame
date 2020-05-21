using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class EntityKilledActionResult : IActionResult
    {
        public readonly IEntity Entity;
        public EntityKilledActionResult(IEntity entity)
        {
            Entity = entity;
        }
    }
}
