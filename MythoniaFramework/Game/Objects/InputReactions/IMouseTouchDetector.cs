using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Objects.InputReactions
{
    public interface IMouseTouchDetector
    {
        public bool IsTouch(MVector mousePosition);
    }
}
