﻿using Mumble.FirstGame.Core;
using Mumble.FirstGame.Core.Action;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.ConsoleApp
{
    public class TagHandler
    {
        //TODO - move to json
        private static Dictionary<TagId, string> _tagTranslation = new Dictionary<TagId, string>
        {
            { TagId.enemy_killed,"{0} has been killed!" },
            { TagId.damage_taken, "{0} has taken {1} damage" },
            { TagId.hp_display,"{0} Health: {1}/{2}" },
            { TagId.damage_display,"{0} has {1} damage" }
        };
        
        public static string TranslateTag(Tag tag)
        {
            if (!_tagTranslation.ContainsKey(tag.Id))
            {
                throw new Exception("Tag Id Not Found");
            }
            return string.Format(_tagTranslation[tag.Id], tag.Arguments);
        }
    }
}
