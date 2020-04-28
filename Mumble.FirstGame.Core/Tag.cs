using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core
{
    public class Tag
    {
        public string Id { get; set; }
        public object[] Arguments { get; set; }

        public Tag(string id, params object[] arguments)
        {
            Id = id;
            Arguments = arguments;
        }
    }
}
