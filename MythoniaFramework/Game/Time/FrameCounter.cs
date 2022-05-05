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


        private static List<float> TimePerFrameRecord = new();

        private static float TimeCount = 0;

        public static float AverageFPS = 0;

        public static void UpdateNewFrame(GameTime gameTime)
        {
            FrameCount++;
            float time = gameTime.ElapsedGameTime.ToStandardFrame();
            //记录下当前帧时长(单位F), 加入RecordList, 增加Count
            TimePerFrameRecord.Add(time);
            TimeCount += time;


            while(TimeCount > MTime.StandardFrameRate)
            {  //让Count保持在略小于标准帧率60以内
                TimeCount -= TimePerFrameRecord[0];
                TimePerFrameRecord.RemoveAt(0);
            }

            if (FrameCount % 10 == 0)
                AverageFPS =
                TimePerFrameRecord.Count / //     这一段时间内循环了多少帧
                TimeCount *                //除以 这一段时间有多长
                MTime.StandardFrameRate;   //乘以 标准帧率
            
        }
    }
}
