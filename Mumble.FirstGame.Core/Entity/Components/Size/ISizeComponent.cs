using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Entity.Components.Size
{
    public interface ISizeComponent
    {

        //Scale - for example, 1 = base pixel square size (i.e. 16x16)
        // 2 = 32x32
        int Scale
        {
            get;
        }
    }
}
