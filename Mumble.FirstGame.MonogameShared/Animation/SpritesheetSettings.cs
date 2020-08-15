using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.MonogameShared.Animation
{
    // Spritesheet settings, which indicate things like which row on spritesheet this animation type resides in
    // defaults are for a 4x1 spritesheet with no color changes (likely the idle spritesheet row)
    public class SpritesheetSettings
    {
        //No spritesheet change, only color change
        public bool ColorChangeOnly = false;
        public Color ColorChange = Color.DarkGray;

        //How to determine the facing, aka which column to use
        //Has facing options, aka 4 columns
        public enum FacingBasisEnum
        {
            Mouse,
            PastMovement,
            NoFacing
        }
        public FacingBasisEnum FacingBasis = FacingBasisEnum.PastMovement;

        //Rows of animation frames, currently must be sequential
        public List<int> Rows = new List<int>()
        {
            0
        };

        //Frame delay between switching to a new row/disabling animation
        public int FrameDelay = 0;

    }
}
