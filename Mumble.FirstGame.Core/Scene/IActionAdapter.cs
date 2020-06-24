using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene
{
    public interface IActionAdapter
    {
        bool TryHandleAction(IAction action, IOwnerIdentifier owner);
    }
}
