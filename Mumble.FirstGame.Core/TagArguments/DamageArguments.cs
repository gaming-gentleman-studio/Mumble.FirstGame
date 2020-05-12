using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.TagArguments
{
    public struct DamageArguments : ITagArguments
    {
        public readonly string TargetName;
        public readonly int DamageTaken;
        public DamageArguments(string targetName, int damageTaken)
        {
            TargetName = targetName;
            DamageTaken = damageTaken;
        }
    }
}
