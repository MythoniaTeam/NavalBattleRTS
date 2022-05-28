


namespace Mythonia.Resources.Texture
{
    public class TextureSet : TextureBase
    {
        /// <summary>图集每行有多少帧</summary>
        public int FramePerRow { get; set; }
        /// <summary>图集一共有多少帧</summary>
        public int FrameCount { get; set; }

        /// <summary>默认动画名称</summary>
        public string DefaultAnimation { get; set; } = "Normal";
        /// <summary>动画数据</summary>
        public Animation[] Animations { get; set; }


        public TextureSet() { }
        public TextureSet(string name) : base(name) { }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="texture"></param>
        public override void LoadInitialize(MGame game, Texture2D texture)
        {
            base.LoadInitialize(game, texture);
            foreach (Animation ani in Animations) ani.LoadInitialize(game);

        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns><inheritdoc/></returns>
        public override TextureBase ProcessData()
        {
            foreach (Animation ani in Animations) ani.ProcessData();
            return base.ProcessData();
        }

        /// <summary>
        /// 给定帧编号, 返回该帧的绘制范围
        /// </summary>
        /// <param name="frameNo">第几帧动画编号</param>
        /// <returns><seealso cref="Rectangle"/> 类型, 表示该帧在贴图中的范围</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public Rectangle GetSourceRange(int frameNo)
        {
            if (frameNo >= FrameCount) throw new IndexOutOfRangeException($"TextureSet \"{Name}\" doesn't have frame #{frameNo}, it has only {FrameCount} frames");
            return new Rectangle(
                (frameNo % FramePerRow, frameNo / FramePerRow) * FrameSize,
                FrameSize);
        }

        /// <summary>
        /// 给定动画名称, 返回该动画的数据
        /// </summary>
        /// <param name="aniName">动画的名称</param>
        /// <returns><seealso cref="Animation"/> 类型, 存有该动画的对应数据</returns>
        /// <exception cref="ObjectNotFoundException"></exception>
        public Animation GetAnimation(string aniName)
        {
            foreach (Animation animation in Animations)
                if (animation.Name == aniName)
                    return animation;
            throw new ObjectNotFoundException("Animation", aniName);
        }
    }
}
