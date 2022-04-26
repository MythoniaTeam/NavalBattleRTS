


namespace Mythonia.Framework.Game.Objects.Actions
{
    public class MAction<ArgsType> : IAction<ArgsType> where ArgsType : IActionArg<ArgsType>
    {
        /*
         * 如5F(标准帧) 走20px => 4px/F
         * DurationMax          = 5
         * Args(NumericalArg)   = 20
         * Action               = Walk
         * 
         * 调用UpdateAction
         * DurationCount    = 0
         * frameDuration    = 1.2
         * 
         * actionRate       = ActionRate(1.2) => 1.2 / 5 => 0.24
         * Action.Invoke(Args.MultipleRate(actionRate))
         *      => Walk(20 * 0.24)
         *      => 行走 4.8px
         *      
         * 4px/F * 1.2F = 4.8px
         */


        /*private string _name;
        public string Name { get => _name, private set => _name = value; }*/



        /// <summary>动作 需要持续的 标准帧时长 (1/60秒)</summary>
        private float _durationMax;
        /// <summary>动作 已执行的时长 (1/60秒)</summary>
        private float _durationCount;

        /// <summary>动作的参数 (如步行10单位, 就是float Args = 10)</summary>
        private ArgsType _arg;

        /// <summary>
        /// 传入ActionArgs, 表示action的参数
        /// </summary>
        private Func<ArgsType, bool> _action;



        public float Duration => _durationMax;
        public float DurationCount
        {
            get => _durationCount;
            protected set => _durationCount = value;
        }
        public ArgsType Arg => _arg;
        public Func<ArgsType, bool> Main
        {
            get => _action;
            set => _action = value;
        }


        public MAction(float duration, ArgsType actionArgs, Func<ArgsType, bool> action = null)//, string name)
        {
            //Name = name;
            _durationMax = duration;
            _arg = actionArgs;
            _action = action;

            this.LogConstruct(true);
        }

        public bool ActionUpdate(GameTime gameTime)
        {
            //本帧 所持续的时间(单位F)
            float frameDuration = gameTime.ElapsedGameTime.ToStandardFrame();
            //本帧 所需要执行动作的 多少部分 (如需要走10步中的2步, rate将会是1/5)
            float actionRate = ActionRate(frameDuration);

            this.Log(true, $"ActionUpdate: FrameDuration: {frameDuration}, ActionRate: {actionRate}");
            //调用Main, 并传递 *Rate 后的Arg
            Main.Invoke(Arg.MultipleRate(actionRate));

            //DurationCount加上本帧的时长
            DurationCount += frameDuration;
            return DurationCount >= Duration;
        }

        /// <summary>
        /// 返回当前帧 执行动作的幅度 
        /// </summary>
        /// <param name="frameDuration"></param>
        /// <returns></returns>
        protected virtual float ActionRate(float frameDuration)
        {
            if (frameDuration > Duration - DurationCount) 
            {
                //如果超出执行时间, 即 当前帧时长 > 需求时长 - 已执行时长
                return 1 - DurationCount / Duration; 
                //执行剩下的部分 [(Max - Count) / Max] == (1 - Count / Max)
            }
            else
            {
                return frameDuration / Duration; 
                //除以DurationMax => 每单位帧的 动作幅度, 乘以frameDuration => 当前帧 和 标准帧 时长之比
            }
        }

    }
}
