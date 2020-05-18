using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Attack;
using Mumble.FirstGame.Core.Scene;
using Mumble.FirstGame.Core.Scene.Battle;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.ConsoleApp
{
    public class ConsoleGameRunner
    {
        private IScene _scene;
        public ConsoleGameRunner(IScene startingScene)
        {
            _scene = startingScene;
        }
        public void Run()
        {
            if (_scene is BattleScene)
            {
                RunBattleScene((BattleScene) _scene);
            }
            Console.WriteLine("Scene end");
        }
        private void RunBattleScene(BattleScene scene)
        {
            while (scene.IsSceneActive())
            {
                IAction action;

                if (YN("Do you want to attack?"))
                {
                    action = new AttackAction(scene.PlayerTeam[0], scene.EnemyTeam[0]);

                }
                else
                {
                    action = new NullAction();
                }
            }
        }
        private bool YN(string prompt)
        {
            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            if ((Char.ToUpper(response[0]) == 'Y') || (response.ToUpper() == "YES"))
            {
                return true;
            }
            return false;
        }
    }
}
