


namespace Mythonia.Game.Objects.Interfaces
{
    public interface IAlignedRectangle : IRectangle
    {
        /// <summary>
        /// 对齐的参照对象 (Size应该大于自身对象)
        /// </summary>
        public IRectangle RefObj { get; }

        /// <summary>
        /// 自身的对齐点
        /// </summary>
        public MVector OriginPosScale => RefAlignPosScale;
        private MVector OriginRelPos => GetPointRelPos(OriginPosScale);

        /// <summary>
        /// 对齐 <see cref="RefObj"/> 的基础点 <br/>
        /// (如对齐RefObj的左上角, 则是 (-1, 1))
        /// </summary>
        public MVector RefAlignPosScale { get; }
        /// <summary>
        /// 在基础点之上，朝中心大概方向的位移
        /// </summary>
        public MVector RefAlignPosDisplacement => (0, 0);

        private MVector RefAlignPos => RefObj.DirectFrom(RefAlignPosScale).To(VecDir.Center).For(RefAlignPosDisplacement);

        private new MVector Position => RefAlignPos - OriginRelPos;
        float IRectangle.CenterX => Position.X;
        float IRectangle.CenterY => Position.Y;


    }
}
