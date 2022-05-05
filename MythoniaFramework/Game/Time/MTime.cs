using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Time
{
    public class MTime
    {
        private static int _standardFrameRate = 60;
        public static int StandardFrameRate
        {
            get => _standardFrameRate;
            protected set => _standardFrameRate = value;
        }

        public MTime(int frameRate)
        {
            StandardFrameRate = frameRate;
        }


        public static float Second_to_StandardFrame(float second) => second * StandardFrameRate;
        public static float Millisecond_to_StandardFrame(float millisecond) => millisecond * StandardFrameRate;

        public static float ToStandardFrame(TimeSpan time) => Second_to_StandardFrame((float)time.TotalSeconds);



    }

    public static class ETime
    {
        public static float ToStandardFrame(this TimeSpan v) => MTime.ToStandardFrame(v);

        public static float FramePerSecond(this GameTime v) => MTime.StandardFrameRate / v.ElapsedGameTime.ToStandardFrame();
        //public static float TotalFrame;
    }
}
