using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.TagArguments
{
    public struct HPArguments : ITagArguments
    {
        public readonly string SubjectName;
        public readonly int CurrentHP;
        public readonly int MaxHP;

        public HPArguments(string subjectName, int currentHP, int maxHP)
        {
            SubjectName = subjectName;
            CurrentHP = currentHP;
            MaxHP = maxHP;
        }
    }
}
