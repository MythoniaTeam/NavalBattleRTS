


namespace Mythonia.Game.Time
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

}
