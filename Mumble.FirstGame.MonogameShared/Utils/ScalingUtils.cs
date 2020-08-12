using Microsoft.Xna.Framework;
using Mumble.FirstGame.MonogameShared.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Utils
{
    public class ScalingUtils
    {
        private readonly IGameSettings _settings;
        private float _windowScale;
        public ScalingUtils(IGameSettings settings, float windowScale)
        {
            _settings = settings;
            _windowScale = windowScale;

        }
        public  Vector2 ScalePosition(Vector2 position)
        {
            return new Vector2((position.X * _settings.SpritePixelSpacing * _windowScale) + _settings.BorderPixelSize,
                (position.Y * _settings.SpritePixelSpacing * _windowScale) + _settings.BorderPixelSize);
        }
        public  Vector2 DescalePosition(Vector2 position)
        {
            return new Vector2((position.X - _settings.BorderPixelSize) / (_settings.SpritePixelSpacing * _windowScale),
                (position.Y - _settings.BorderPixelSize) / (_settings.SpritePixelSpacing * _windowScale));
        }
        public  Vector2 ScaleSize(Vector2 scale)
        {
            return new Vector2(scale.X * _settings.ScreenScale * _windowScale, scale.Y * _settings.ScreenScale * _windowScale);
        }
    }
}
