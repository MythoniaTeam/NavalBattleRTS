


namespace Mythonia.Game.Objects.Draw
{
    public class AnimationPlayer : ITexture, IMClass
    {
        #region Implement - IMClass

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        #endregion



        #region Props

        private TextureSet TextureAnimated { get; }

        private float _timeCount;
        /// <summary>记录当前循环时长的计时器(以标准帧F为单位)</summary>
        public float TimeCount
        {
            get => _timeCount;
            set
            {
                //计时器超过一次循环时, 取余, 开启新循环
                _timeCount = value;
                _timeCount %= CurrentAnimation.CycleDuration;
            }
        }



        /// <summary>当前应该绘制的帧</summary>
        public int CurrentFrame
        {
            // 计时器 / 每帧时长(已变速)  => 当前动画帧编号
            get => (int)(TimeCount / FrameDurationChanged);
            // 输入动画帧编号 => 计时器设为 编号 * 每帧时长(已变速)
            set => TimeCount = FrameDurationChanged * value;
        }
        private Animation CurrentAnimation;



        /// <summary>乘以速率后的 动画帧时长</summary>
        private float FrameDurationChanged => CurrentAnimation.FrameDuration * PlaySpeed;
        private float _playSpeed = 1;
        /// <summary>播放速度</summary>
        public float PlaySpeed
        {
            get => _playSpeed;
            set
            {
                //记录下当前帧, 在速度改变后, 再把计时器恢复到 = 当前帧的值
                //(需要是float, 来确保修改计时器的时候算上超出的部分，如原Duration = 4F, 计时器6F, 需要 算上多出来的那2F)
                float currentFrame = TimeCount / FrameDurationChanged;
                _playSpeed = value;
                TimeCount = FrameDurationChanged * currentFrame;
            }
        }

        #endregion



        #region Constructors

        public AnimationPlayer(MGame game, string name) { _game = game; _name = name;  }    
        public AnimationPlayer(TextureSet texture, string aniName = null, float playSpeed = 1) : this(texture.MGame, $"AnimationPlayer-{texture.Name}")
        {
            TextureAnimated = texture;
            //if (texture is not TextureSet) throw new Exception($"The Given Texture to SpriteAnimated \"{Name}\" is not TextureSet");

            //如果 aniName 为 null, 将它设为 Texture 的 DefaultAnimation
            aniName ??= TextureAnimated.DefaultAnimation;
            //如果 Default 也是null, 获取第一个动画
            CurrentAnimation = (aniName is not null) ? TextureAnimated.GetAnimation(aniName) : TextureAnimated.Animations[0];

            PlaySpeed = playSpeed;
        }

        #endregion



        #region Methods

        /// <summary>
        /// 每帧Update调用的函数, 用于更新贴图TimeCount
        /// </summary>
        /// <param name="gameTime"></param>
        public void UpdateAnimation(GameTime gameTime)
        {
            TimeCount += gameTime.ElapsedGameTime.ToStandardFrame();
        }


        /// <summary>
        /// 给定动画名称, 设置播放的动画
        /// </summary>
        /// <param name="aniName">动画名称</param>
        /// <param name="playSpeed">播放速度, 默认为1</param>
        /// <exception cref="ObjectNotFoundException"></exception>
        public void SetAnimation(string aniName, float playSpeed = 1)
        {
            CurrentAnimation = TextureAnimated.GetAnimation(aniName);
            TimeCount = 0;
            PlaySpeed = playSpeed;
        }

        #endregion



        #region Implement - ITexture 

        Texture2D ITexture.SourceTexture => TextureAnimated.SourceTexture;

        /// <summary>
        /// <inheritdoc/>
        /// <para>
        /// <i>根据 <see cref="CurrentFrame"/>(当前帧), 调用 <see cref="TextureAnimated"/>(贴图资源) 的 GetSourceRange(..) 方法</i>
        /// </para>
        /// </summary>
        Rectangle ITexture.SourceRange => TextureAnimated.GetSourceRange(CurrentFrame);

        MVector ITexture.TextureBasicScale => TextureAnimated.TextureBasicScale;

        MVector ITexture.TextureOrigin => TextureAnimated.Origin;

        #endregion
    }
}
