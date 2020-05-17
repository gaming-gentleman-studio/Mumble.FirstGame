using Mumble.FirstGame.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult
{
    public class EntitiesCreatedActionResult : IActionResult
    {
        public List<IEntity> Entities { get; private set; }
        public EntitiesCreatedActionResult(List<IEntity> entities)
        {
            Entities = entities;
        }
    }
}
