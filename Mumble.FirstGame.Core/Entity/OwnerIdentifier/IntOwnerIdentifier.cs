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
    }
}
