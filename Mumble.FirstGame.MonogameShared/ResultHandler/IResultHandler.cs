using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.ResultHandler
{
    public interface IResultHandler
    {
        void ApplyResults(List<IActionResult> results);
    }
}
