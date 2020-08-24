using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.DisplayHandlers.Menu
{
    public class MainMenuDisplayHandler : IDisplayHandler
    {
        private SpriteBatch _spriteBatch;
        private ContentImages _images;
        private List<AbstractSpriteMetadata> _uiSprites;
        private List<MenuItemMetadata> _menuItems;
        public MainMenuDisplayHandler(GraphicsDeviceManager graphics, ContentImages images, List<MenuItemMetadata> menuItems, List<AbstractSpriteMetadata> uiSprites)
        {
            _spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            _menuItems = menuItems;
            _uiSprites = uiSprites;
            _images = images;
        }
        public SpriteBatch Draw(Vector2 mousePosition)
        {
            _spriteBatch.Begin();
            foreach(MenuItemMetadata item in _menuItems)
            {
                _spriteBatch.DrawString(item.Font, item.Text, item.Position, Color.Red,0f,item.Origin,1f,SpriteEffects.None,0f);
            }
            foreach (AbstractSpriteMetadata sprite in _uiSprites)
            {
                _spriteBatch.Draw(sprite.GetImage(_images),
                    sprite.GetPosition(),
                    sprite.GetSpritesheetRectange(mousePosition),
                    sprite.GetColor(),
                    sprite.GetRotation(),
                    sprite.GetOrigin(),
                    sprite.GetScale(),
                    SpriteEffects.None, 0f);
            }
            _spriteBatch.End();
            return _spriteBatch;
        }
    }
}
