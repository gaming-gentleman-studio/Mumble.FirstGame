using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Enemy;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.Core.Scene.Battle;
using System;
using System.Collections.Generic;

namespace Mumble.FirstGame.ConsoleApp
{
    class Program
    {
        //No DI container for now
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Nate");
            Player player = new Player(3, 10);
            Slime slime = new Slime(2, 4);
            BattleScene scene = new BattleScene(
                new List<ICombatEntity>() { player },
                new List<ICombatAIEntity>() { slime });
            ConsoleGameRunner runner = new ConsoleGameRunner(scene);
            runner.Run();
        }
    }
}
