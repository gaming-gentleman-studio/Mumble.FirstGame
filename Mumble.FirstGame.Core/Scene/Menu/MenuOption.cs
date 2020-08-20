using Mumble.FirstGame.Core.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Scene.Menu
{
    public class MenuOption
    {
        public static MenuOption Default = new MenuOption(true);
        private string _displayText;
        public IAction Action { get; private set; }
        public bool IsDefault = false;

        private MenuOption(bool isDefault)
        {
            IsDefault = isDefault;
        }
        public MenuOption(string displayText,IAction action)
        {
            _displayText = displayText;
            Action = action;
        }
        public override string ToString()
        {
            return _displayText;
        }
    }
}
