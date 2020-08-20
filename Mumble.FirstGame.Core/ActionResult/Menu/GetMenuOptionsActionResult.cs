using Mumble.FirstGame.Core.Scene.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.ActionResult.Menu
{
    public class GetMenuOptionsActionResult : IActionResult
    {
        public List<MenuOption> Options { get; private set; }
        public GetMenuOptionsActionResult(List<MenuOption> options)
        {
            Options = options;
        }
    }
}
