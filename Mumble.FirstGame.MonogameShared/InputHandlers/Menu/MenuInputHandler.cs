using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Menu;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.InputHandlers.Menu
{
    public class MenuInputHandler : IInputHandler
    {
        private List<MenuItemMetadata> _menuItems;
        public MenuInputHandler(List<MenuItemMetadata> menuItems)
        {
            _menuItems = menuItems;
        }
        public List<IAction> HandleInput()
        {
            List<IAction> actions = new List<IAction>();
            if (!(Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                return actions;
            }
            Point mousePosition = Mouse.GetState().Position;
            
            foreach (MenuItemMetadata item in _menuItems)
            {
                if (item.Bounds.Contains(mousePosition))
                {
                    actions.Add(new ClickMenuItemAction(item.Option));
                }
            }
            return actions;
        }
    }
}
