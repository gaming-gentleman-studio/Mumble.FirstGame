using Mumble.FirstGame.Core.TagArguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core
{
    public class Tag
    {
        public TagId Id { get; private set; }

        public ITagArguments Arguments { get; private set; } 

        public Tag(TagId id, ITagArguments arguments)
        {
            Id = id;
            Arguments = arguments;
        }

    }
}
