using Mumble.FirstGame.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Settings
{
    public class GameSettings : IGameSettings
    {
        public IPEndPoint Server => new IPEndPoint(IPAddress.Parse("127.0.0.1"),27000);

        public ClientType ClientType => ClientType.Solo;

        public TimeSpan TickRate => TimeSpan.FromMilliseconds(50);

        public bool FullScreen => false;
    }
}
