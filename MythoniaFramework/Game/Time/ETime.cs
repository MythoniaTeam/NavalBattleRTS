


namespace Mythonia.Game.Time
{

    public static class ETime
    {
        public static float ToStandardFrame(this TimeSpan v) => MTime.ToStandardFrame(v);

        public static float FramePerSecond(this GameTime v) => MTime.StandardFrameRate / v.ElapsedGameTime.ToStandardFrame();
        //public static float TotalFrame;
    }
}
