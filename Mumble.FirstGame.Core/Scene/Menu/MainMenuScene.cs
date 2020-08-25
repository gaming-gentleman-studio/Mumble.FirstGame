using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Menu;
using Mumble.FirstGame.Core.Action.Meta;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Menu
{
    public class MainMenuScene : IScene
    {
        private Director _director;
        
        public MainMenuScene(Director director)
        {
            _director = director;
        }
        public bool IsSceneActive()
        {
            return true;
        }

        public List<IActionResult> Update(Dictionary<IOwnerIdentifier, List<IAction>> actions, int elapsedTicks)
        {
            List<IAction> resultingActions = new List<IAction>();
            foreach(IOwnerIdentifier owner in actions.Keys)
            {
                foreach (IAction action in actions[owner])
                {
                    if (action is IRequestMenuOptionsAction)
                    {
                        IRequestMenuOptionsAction menuAction = (IRequestMenuOptionsAction)action;
                        menuAction.CalculateEffect();
                        resultingActions.Add(menuAction);
                    }
                    else if (action is EnterSceneAction)
                    {
                        EnterSceneAction enterAction = (EnterSceneAction)action;
                        enterAction.CalculateEffect();
                        resultingActions.Add(enterAction);

                        RequestMenuOptionsAction menuAction = new RequestMenuOptionsAction(MenuOption.Default);
                        menuAction.CalculateEffect();
                        resultingActions.Add(menuAction);
                    }
                    else if (action is ExitGameAction)
                    {
                        ExitGameAction exitAction = (ExitGameAction)action;
                        exitAction.CalculateEffect();
                        resultingActions.Add(exitAction);
                    }
                    else if (action is LoadSceneAction)
                    {
                        LoadSceneAction loadAction = (LoadSceneAction)action;
                        loadAction.CalculateEffect(_director);
                        resultingActions.Add(loadAction);
                    }
                    else if (action is ClickMenuItemAction)
                    {
                        ClickMenuItemAction clickAction = (ClickMenuItemAction)action;
                        clickAction.CalculateEffect(owner, _director.CurrentScene);
                        resultingActions.Add(clickAction);
                    }
                }
            }

            List<IActionResult> results = new List<IActionResult>();
            foreach(IAction action in resultingActions)
            {
                results.AddRange(action.Results);
            }
            return results;
        }
    }
}
