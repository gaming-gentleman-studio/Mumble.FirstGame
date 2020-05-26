using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mumble.FirstGame.Serialization.Protobuf.Factory
{
    public class ActionFactory : IActionFactory
    {
        private IEntityContainer _entityContainer;

        public ActionFactory(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
        }

        
        public IAction Create(byte[] data)
        {
            int type = data[0];
            byte[] serializedAction = data.Skip(1).Take(data.Length).ToArray();
            IAction action = null;
            switch (type)
            {
                case Lookup.Move:
                    MoveActionDef moveDef = MoveActionDef.Parser.ParseFrom(serializedAction);
                    action = new MoveAction((IMoveableEntity)_entityContainer.GetEntity(moveDef.Id), Lookup.SerializedToDirectionMap[moveDef.Direction]);
                    break;
                case Lookup.Fire:
                    FireActionDef fireDef = FireActionDef.Parser.ParseFrom(serializedAction);
                    action = new FireWeaponAction((ICombatEntity)_entityContainer.GetEntity(fireDef.Id), Lookup.SerializedToDirectionMap[fireDef.Direction]);
                    break;
                default:
                    throw new Exception("Improper action found while attempting to deserialize");
            }
            return action;
        }
    }
}
