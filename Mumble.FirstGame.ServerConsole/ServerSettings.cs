using Mumble.FirstGame.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.ServerConsole
{
    public class ServerSettings : IServerSettings
    {
        public int ServerPort => 27000;
    }
}
