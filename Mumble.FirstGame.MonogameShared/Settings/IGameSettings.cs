using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Settings
{
    public interface IGameSettings
    {
        IPEndPoint Server { get; }
    }
}
