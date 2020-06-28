using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Server
{
    public interface IServerSettings
    {
        int ServerPort { get; }

        TimeSpan TickRate { get; }

        int UpdateCyclesPerTick { get; }
    }
}
