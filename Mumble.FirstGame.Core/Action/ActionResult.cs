using Mumble.FirstGame.Core.TagArguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Action
{
    public class ActionResult
    {
        public Tag Tag { get; private set; }

        public ActionResult(TagId tagId, ITagArguments arguments)
        {
            Tag = new Tag(tagId, arguments);
        }
    }
}
