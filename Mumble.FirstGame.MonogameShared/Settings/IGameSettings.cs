using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Settings
{
    public interface IGameSettings
    {
        IPEndPoint Server { get; }

        ClientType ClientType { get; }

        TimeSpan TickRate { get; }

        bool FullScreen { get; }

        Tuple<int, int> AspectRatio { get; }
        int SpritePixelSpacing { get; }

        int ScreenScale { get; }

        int BorderPixelSize { get; }



        

    }
}
