


namespace Mythonia.Game.Objects
{
   
    public class RelativePos
    {
        /// <summary>
        /// UI对齐窗口的位置 (中心为0 y朝上 相对尺寸坐标)<br/>
        /// default: (-1, 1) 即 左上角
        /// </summary>
        public MVector Align { get; private set; } = VecDir.TopLeft;
        /// <summary>
        /// UI Origin相对Align的偏移 (y朝上 绝对坐标)<br/>
        /// default: (20, 10) * align.GetSign;
        /// </summary>
        public MVector Offset { get; private set; } = new(20, 10);
        /// <summary>
        /// UI的Origin相对 贴图中心点位置 (中心为0 y朝上 相对尺寸坐标)<br/>
        /// default: (-1, 1) 即 贴图左上角
        /// </summary>
        public MVector Origin { get; private set; } = new(-1, 1);
        public MVector ScreenPosition => Align * _screen.Size / 2
                + Offset - Origin * _getSize() / 2;
        
        
        protected readonly Screen _screen;

        protected readonly Func<MVector> _getSize;

        public RelativePos(Func<MVector> getSize, Screen screen,
            MVector? align = null, MVector? offset = null, MVector? origin = null)
        {
            if (align is MVector align2) Align = align2;

            if (offset is MVector offset2) Offset = offset2;
            Offset *= -(MVector)Align.GetSign;

            if (origin is MVector origin2) Origin = origin2;
            else Origin = Align.GetSign;

            _getSize = getSize;
            _screen = screen;
        }
    }
}
