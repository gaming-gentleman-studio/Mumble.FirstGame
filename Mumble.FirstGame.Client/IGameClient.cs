using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Client
{
    public interface IGameClient
    {
        IScene CurrentScene { get; }

        IOwnerIdentifier Owner { get; }
        List<IActionResult> Update(List<IAction> actions);

        void Register();
        List<IActionResult> Init();

       
    }
}
