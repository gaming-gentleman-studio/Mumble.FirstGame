using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Background
{
    public class WallStyle
    {
        public WallDecor Decor { get; private set; }

        public WallDoodadSet DoodadSet { get; private set; }

        public WallStyle(WallDecor decor, WallDoodadSet doodad)
        {
            Decor = decor;
            DoodadSet = doodad;
        }
    }
    public enum WallDecor
    {
        Dungeon,
    }
    public enum WallDoodadSet
    {
        None,
        FracturedTop
    }
}
