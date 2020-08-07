using System;
using System.Collections.Generic;
using System.Text;

namespace Mumble.FirstGame.Core.Background
{
    public interface IBackground
    {
        bool HasCollision { get; }
        BackgroundSubType SubType { get; }
    }
}
