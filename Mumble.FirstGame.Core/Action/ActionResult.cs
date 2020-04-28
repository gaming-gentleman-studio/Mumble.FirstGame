using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    public class ActionResult
    {
        public Tag Tag { get; private set; }

        public ActionResult(string tagId, params object[] arguments)
        {
            Tag = new Tag(tagId, arguments);
        }
    }
}
