using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Time
{
    public class FrameCounter
    {
        private static int _frameCount;

        public static int FrameCount
        {
            get => _frameCount;
            private set => _frameCount = value;
        }

        public static void UpdateNewFrame() => FrameCount++;
    }
}
