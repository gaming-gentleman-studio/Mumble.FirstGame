using Microsoft.Xna.Framework;
using Mumble.FirstGame.MonogameShared.Animation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared
{
    public class AnimationHandlerSettings
    {
        
        public Dictionary<AnimationTypes, SpritesheetSettings> SpritesheetSettingsMap;

        // Precedence of animation types - animations will be ran in the order listed here, and the first to run is the only to apply
        // Cut sections of this list if you do not have animation types defined, i.e. if your sprite does not have an attack animation
        // Idle is required, but is often the same row on the spritesheet as the first row of the move animation 
        public List<AnimationTypes> AnimationTypePrecedence = new List<AnimationTypes>()
        {
            AnimationTypes.Damage,
            AnimationTypes.Attack,
            AnimationTypes.Move,
            AnimationTypes.Idle
        };

    }
}
