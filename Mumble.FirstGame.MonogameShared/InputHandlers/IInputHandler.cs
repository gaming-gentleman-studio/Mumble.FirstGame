using Mumble.FirstGame.Core.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.InputHandlers
{
    public interface IInputHandler
    {
        List<IAction> HandleInput();
    }
}
