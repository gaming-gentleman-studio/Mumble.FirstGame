using Mumble.FirstGame.Server;
using System;
using System.Net;

namespace Mumble.FirstGame.ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 27000);
            UdpServer server = new UdpServer(endpoint);
            server.Listen();
            Console.ReadKey();
        }
    }
}
