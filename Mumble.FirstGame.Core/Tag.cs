using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core
{
    public class Tag
    {
        public string Id { get; private set; }
        public object[] Arguments { get; private set; }

        public Tag(string id, params object[] arguments)
        {
            Id = id;
            Arguments = arguments;
        }
    }
}
