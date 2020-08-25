using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Player;
using Mumble.FirstGame.MonogameShared.SpriteMetadata;
using Mumble.FirstGame.MonogameShared.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.InputHandlers
{
    public class BattleInputHandler : IInputHandler
    {
        private Player _player;
        private ScalingUtils _scalingUtils;
        private Dictionary<IEntity, AbstractSpriteMetadata> _entitySprites;
        MovementKeyHandler _keyHandler;
        MouseHandler _mouseHandler;
        public BattleInputHandler(Player player, ScalingUtils scalingUtils, Dictionary<IEntity, AbstractSpriteMetadata> entitySprites)
        {
            _player = player;
            _scalingUtils = scalingUtils;
            _entitySprites = entitySprites;
            _keyHandler = new MovementKeyHandler();
            _mouseHandler = new MouseHandler();

        }
        public List<IAction> HandleInput()
        {
            List<IAction> actions = new List<IAction>();
            if (_entitySprites.ContainsKey(_player))
            {
                actions.AddIfNotNull(_keyHandler.HandleKeyPress(_player));
                actions.AddIfNotNull(_mouseHandler.HandleMouseClick(_player, _scalingUtils.ScalePosition(_entitySprites[_player].GetPosition())));
            }
            return actions;
        }
    }
}
