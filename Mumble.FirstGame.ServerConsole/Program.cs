using Mumble.FirstGame.Server;
using System;
using System.Net;

namespace Mumble.FirstGame.ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ServerManager server = new ServerManager();
            server.Listen();
            Console.ReadKey();
        }
    }
}
