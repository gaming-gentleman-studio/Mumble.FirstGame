using Microsoft.Extensions.DependencyInjection;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Server;
using System;
using System.Net;

namespace Mumble.FirstGame.ServerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceProvider provider = RegisterServices();
            ServerManager server = new ServerManager(provider.GetService<IScene>(),provider.GetService<IServerSettings>());
            server.Listen();
            Console.ReadKey();
        }
        private static IServiceProvider RegisterServices()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<IServerSettings, ServerSettings>()
                .AddSingleton<IEntityContainer, BattleEntityContainer>()
                .AddTransient<IActionAdapter, OnlineActionAdapter>()
                .AddSingleton<IScene,BattleScene>()
                
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
