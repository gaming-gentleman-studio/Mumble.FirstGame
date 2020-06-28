using Microsoft.Extensions.DependencyInjection;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Factory;
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
            ServerManager server = new ServerManager(provider.GetService<IScene>(),provider.GetService<IServerSettings>(), provider.GetService<IActionFactory>());
            server.Listen();
            Console.ReadKey();
        }
        private static IServiceProvider RegisterServices()
        {
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<IServerSettings, ServerSettings>()
                .AddSingleton<IEntityContainer, BattleEntityContainer>()
                .AddTransient<IActionAdapter, OnlineActionAdapter>()
                .AddTransient<IActionFactory, ActionFactory>()
                .AddTransient<IActionResultFactory,ActionResultFactory>()
                .AddTransient<IEntityFactory,EntityFactory>()
                .AddSingleton<IScene,BattleScene>()
                
                .BuildServiceProvider();
            return serviceProvider;
        }
    }
}
