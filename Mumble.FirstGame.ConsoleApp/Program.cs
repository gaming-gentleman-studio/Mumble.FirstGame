using Mumble.FirstGame.Client;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene.Battle;
using System;
using System.Collections.Generic;
using System.Net;

namespace Mumble.FirstGame.ConsoleApp
{
    class Program
    {
        //No DI container for now
        static void Main(string[] args)
        {
            OnlineGameClient client = new OnlineGameClient(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 27000));
            while (true)
            {
                client.Send("Hi there");
                client.Receive();
                Console.ReadKey();

            }
        }
    }
}
