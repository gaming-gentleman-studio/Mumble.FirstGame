using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.TagArguments
{
    public struct EntityKilledArguments : ITagArguments
    {
        public readonly string TargetName;
        public EntityKilledArguments(string targetName)
        {
            TargetName = targetName;
        }
    }
}
