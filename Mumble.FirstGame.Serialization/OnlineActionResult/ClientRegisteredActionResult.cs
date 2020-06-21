using Mumble.FirstGame.Core.ActionResult;
using Mumble.FirstGame.Core.Entity.OwnerIdentifier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Serialization.OnlineActionResult
{
    public class ClientRegisteredActionResult : IActionResult
    {
        public IOwnerIdentifier OwnerIdentifier { get; private set; }

        public ClientRegisteredActionResult(IOwnerIdentifier identifier)
        {
            OwnerIdentifier = identifier;
        }
    }
}
