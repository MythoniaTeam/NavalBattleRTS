

namespace Mythonia.Framework.Objects.Actions
{
    public class ContinousAction<ArgsType>
    {
        /// <summary>动作 需要持续的 标准帧时长 (1/60秒)</summary>
        public float DurationMax;
        /// <summary>动作 已执行的时长 (1/60秒)</summary>
        public float DurationCount;

        /// <summary>动作的参数 (如步行10单位, 就是float Args = 10)</summary>
        public ArgsType ActionArgs;

        /// <summary>
        /// 传入ActionArgs, 表示action的参数, 再传入一float参数, 表示当前帧执行的时长
        /// </summary>
        private Func<ArgsType, float, bool> Action;

        public ContinousAction(float duration, ArgsType actionArgs, Func<ArgsType, float, bool> action = null)
        {
            DurationMax = duration;
            ActionArgs = actionArgs;
            Action = action;

        }

        public bool UpdateAction(GameTime gameTime)
        {
            float frameDuration = (float)gameTime.ElapsedGameTime.TotalSeconds * 60;
            DurationCount += frameDuration;
            if (DurationCount >= DurationMax)
            {
                if (DurationCount != DurationMax)
                    Action.Invoke(ActionArgs, frameDuration - (DurationCount - DurationMax)); //执行时间(frame) - 超出的部分(count - max)
                return true;
            }
            else
            {
                Action.Invoke(ActionArgs, frameDuration);
            }

            return false;
        }

        protected virtual float

    }
}
