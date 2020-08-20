using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Mumble.FirstGame.MonogameShared.DisplayHandlers.Battle
{
    public class BattleDisplayHandler : IDisplayHandler
    {
        private SpriteBatch _spriteBatch;
        private ContentImages _contentImages;
        private Dictionary<IEntity, AbstractSpriteMetadata> _entitySprites;
        private List<AbstractSpriteMetadata> _uiSprites;
        private List<AbstractSpriteMetadata> _backgroundSprites;
        private ScalingUtils _scalingUtils;


        public BattleDisplayHandler(GraphicsDeviceManager graphics, ContentImages contentImages, ScalingUtils scalingUtils,Dictionary<IEntity, AbstractSpriteMetadata> entitySprites, List<AbstractSpriteMetadata> uiSprites, List<AbstractSpriteMetadata> backgroundSprites)
        {
            _spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            _contentImages = contentImages;
            _entitySprites = entitySprites;
            _uiSprites = uiSprites;
            _backgroundSprites = backgroundSprites;
            _scalingUtils = scalingUtils;

        }
        public SpriteBatch Draw(Vector2 mousePosition)
        {

            _spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
            
            foreach (AbstractSpriteMetadata sprite in _backgroundSprites)
            {
                _spriteBatch.Draw(sprite.GetImage(_contentImages),
                    _scalingUtils.ScalePosition(sprite.GetPosition()),
                    sprite.GetSpritesheetRectange(mousePosition),
                    sprite.GetColor(),
                    sprite.GetRotation(),
                    sprite.GetOrigin(),
                    _scalingUtils.ScaleSize(sprite.GetScale()),
                    SpriteEffects.None,
                    sprite.GetLayerDepth());
            }
            foreach (IEntity entity in _entitySprites.Keys)
            {
                AbstractSpriteMetadata sprite = _entitySprites[entity];
                _spriteBatch.Draw(sprite.GetImage(_contentImages),
                    _scalingUtils.ScalePosition(sprite.GetPosition()),
                    sprite.GetSpritesheetRectange(mousePosition),
                    sprite.GetColor(),
                    sprite.GetRotation(),
                    sprite.GetOrigin(),
                    _scalingUtils.ScaleSize(sprite.GetScale()),
                    SpriteEffects.None,
                    sprite.GetLayerDepth());
            }
            foreach (AbstractSpriteMetadata sprite in _uiSprites)
            {
                _spriteBatch.Draw(sprite.GetImage(_contentImages),
                    sprite.GetPosition(),
                    sprite.GetSpritesheetRectange(mousePosition),
                    sprite.GetColor(),
                    sprite.GetRotation(),
                    sprite.GetOrigin(),
                    sprite.GetScale(),
                    SpriteEffects.None, 0f);
            }
            //DebugUtils.DrawGrid(spriteBatch, graphics.GraphicsDevice, scalingUtils, scene.Boundary.Width, scene.Boundary.Height);
            _spriteBatch.End();
            return _spriteBatch;
        }
    }
}
