using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.OwnerIdentifier
{
    public class IntOwnerIdentifier : IOwnerIdentifier
    {
        public bool SystemOwned = true;
        public int Id { get; private set; }
        public static IntOwnerIdentifier NonPlayerOwned => new IntOwnerIdentifier();
        private IntOwnerIdentifier()
        {

        }
        public IntOwnerIdentifier(int id)
        {
            Id = id;
            SystemOwned = false;
        }

        public bool PlayerOwned()
        {
            return !SystemOwned;
        }

        public bool Equals(IOwnerIdentifier other)
        {
            if (!(other is IntOwnerIdentifier))
            {
                return false;
            }
            IntOwnerIdentifier otherInt = (IntOwnerIdentifier)other;
            if(otherInt.Id == Id)
            {
                return true;
            }
            return false;
        }
        public static IntOwnerIdentifier FromByte(byte message)
        {
            return new IntOwnerIdentifier((int)message);
        }
    }
}
