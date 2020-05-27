using Mumble.FirstGame.Core.Action;
using Mumble.FirstGame.Core.Action.Fire;
using Mumble.FirstGame.Core.Action.Movement;
using Mumble.FirstGame.Core.Entity;
using Mumble.FirstGame.Core.Entity.Components.Velocity;
using Mumble.FirstGame.Core.Scene.EntityContainer;
using Mumble.FirstGame.Serialization.Protobuf.Action;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Direction = Mumble.FirstGame.Core.Entity.Components.Velocity.Direction;

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
            IAction action = new NullAction();
            switch (type)
            {
                case Lookup.Move:
                    try
                    {
                        MoveActionDef moveDef = MoveActionDef.Parser.ParseFrom(serializedAction);
                        action = new MoveAction((IMoveableEntity)_entityContainer.GetEntity(moveDef.Id), new Direction(moveDef.Direction.Radians));
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to parse move action");
                    }
                    break;
                case Lookup.Fire:
                    try
                    {
                        FireActionDef fireDef = FireActionDef.Parser.ParseFrom(serializedAction);
                        action = new FireWeaponAction((ICombatEntity)_entityContainer.GetEntity(fireDef.Id), new Direction(fireDef.Direction.Radians));
                    }
                    catch
                    {
                        Debug.WriteLine("Failed to parse fire action");

                    }
                    break;
                default:
                    throw new Exception("Improper action found while attempting to deserialize");
            }
            return action;
        }
    }
}
