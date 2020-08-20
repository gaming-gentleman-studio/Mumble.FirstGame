using Mumble.FirstGame.Core.Action.Meta;
using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.ActionResult.Menu;
using Mumble.FirstGame.Core.Scene.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action.Menu
{
    public class RequestMenuOptionsAction : IRequestMenuOptionsAction
    {
        private MenuOption _option;
        public List<IActionResult> Results { get; private set; }

        public RequestMenuOptionsAction(MenuOption option)
        {
            _option = option;
        }

        public void CalculateEffect()
        {
            if (_option.IsDefault)
            {
                Results = new List<IActionResult>(){
                    new GetMenuOptionsActionResult(new List<MenuOption>()
                    {
                        new MenuOption("Start",new EnterSceneAction()),
                        new MenuOption("Exit",new ExitGameAction())
                    })
                };
            }
        }
    }
}
