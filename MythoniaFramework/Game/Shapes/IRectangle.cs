



namespace Mythonia.Game.Shapes
{



    public interface IRectangle
    {
        public IRectangle AsRect { get; }

        public sealed MVector Position => new(XCenter, YCenter);
        public MVector ScreenPosition => Position;


        public sealed MVector Size => new(Width, Height);
        public sealed float Height => (HeightSource, YTopSource, YCenterSource, YBottomSource) switch
        {
            (float h, _,        _,        _       ) =>  h,
            (null,    float yt, _,        float yb) =>  yt - yb,
            (null,    float yt, float yc, _       ) => (yt - yc) * 2,
            (null,    _,        float yc, float yb) => (yc - yb) * 2,

        };
        public sealed float Width => (WidthSource, XLeftSource, XCenterSource, XRightSource) switch
        {
            (float w, _,        _,        _       ) =>  w,
            (null,    float xl, _,        float xr) =>  xl - xr,
            (null,    float xl, float xc, _       ) => (xl - xc) * 2,
            (null,    _,        float xc, float xr) => (xc - xr) * 2,

        };

        protected float? HeightSource => null;
        protected float? WidthSource  => null;

        //public MVector GetPoint(VecDir dir) => Size * dir / 2;

        public sealed float YTop => (YTopSource, YCenterSource, YBottomSource, HeightSource) switch
        {
            (float yt, _,        _,        _)       => yt,
            (null,     float yc, _,        float h) => yc + h / 2,
            (null,     null,     float yb, float h) => yb + h,
            _ => throw new NotImplementedException("The IRectangle isn't Implement Correctly")
        };
        public sealed float YCenter => (YCenterSource, YTopSource, YBottomSource, HeightSource) switch
        {
            (float yc, _,        _,        _)       => yc,
            (null,     float yt, _,        float h) => yt - h / 2,
            (null,     null,     float yb, float h) => yb + h / 2,
            _ => throw new NotImplementedException("The IRectangle isn't Implement Correctly")
        };
        public sealed float YBottom => ( YBottomSource, YTopSource, YCenterSource,HeightSource) switch
        {
            (float yb, _,        _,        _)       => yb,
            (null,     float yt, _,        float h) => yt - h,
            (null,     null,     float yc, float h) => yc - h / 2,
            _ => throw new NotImplementedException("The IRectangle isn't Implement Correctly")
        };
        public sealed float XLeft   => (XLeftSource, XCenterSource, XRightSource, WidthSource) switch
        {
            (float xl, _, _, _) => xl,
            (null, float xc, _, float w) => xc + w / 2,
            (null, null, float xr, float w) => xr + w,
            _ => throw new NotImplementedException("The IRectangle isn't Implement Correctly")
        };
        public sealed float XCenter => (XCenterSource, XLeftSource, XRightSource, WidthSource) switch
        {
            (float xc, _, _, _) => xc,
            (null, float xl, _, float w) => xl - w / 2,
            (null, null, float xr, float w) => xr + w / 2,
            _ => throw new NotImplementedException("The IRectangle isn't Implement Correctly")
        };
        public sealed float XRight  => (XRightSource, XLeftSource, XCenterSource, WidthSource) switch
        {
            (float xr, _, _, _) => xr,
            (null, float xl, _, float w) => xl - w,
            (null, null, float xc, float w) => xc - w / 2,
            _ => throw new NotImplementedException("The IRectangle isn't Implement Correctly")
        };

        protected float? YTopSource => null;
        protected float? YCenterSource => null;
        protected float? YBottomSource => null;
        protected float? XLeftSource => null;
        protected float? XCenterSource => null;
        protected float? XRightSource => null;



        public sealed MVector PointCenter => new(XCenter, YCenter);
        public sealed MVector PointTop => new(XCenter, YTop);
        public sealed MVector PointBottom => new(XCenter, YBottom);
        public sealed MVector PointLeft => new(XLeft, YCenter);
        public sealed MVector PointRight => new(XRight, YCenter);
        public sealed MVector PointTL => new(XLeft, YTop);
        public sealed MVector PointTR => new(XRight, YTop);
        public sealed MVector PointBL => new(XLeft, YBottom);
        public sealed MVector PointBR => new(XRight, YBottom);

        /// <summary>
        /// 给定一个<see cref="MVector"/>比例, 返回对应比例表示的位置
        /// <list type="table">
        /// <item>
        /// <term><paramref name="scale"/></term>
        /// <description><br/>
        /// 一个<see cref="MVector"/>类型的比例, (也可使用<see cref="VecDir"/>), x y ∈ {-1,0,1}<br/>
        /// 以 (0,0) 为中心点, (1,1) 为右上方. 
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="scale"></param>
        /// <returns></returns>
        public sealed MVector Point(MVector scale) => scale / 2 * Size + Position;

        /// <summary>
        /// 调用此方法, 返回自身IRectangle对象和scale后的坐标, 配合扩展方法To(..)使用
        /// </summary>
        /// <param name="scaleFrPt"></param>
        /// <returns></returns>
        public sealed (IRectangle @this, MVector pos) 
            DirectFrom(MVector scaleFrPt) 
            => (this, Point(scaleFrPt));
    }

    public static class EDirectFrom
    {
        /// <summary>
        /// <code>DirectFrom(pointA).To(pointB)</code>
        /// 获取一个从 点A 开始, 朝向 点B 的<see cref="VecDir"/>方向
        /// </summary>
        /// <param name="v"></param>
        /// <param name="scaleToPt"></param>
        /// <returns></returns>
        public static (MVector pos, VecDir dir) 
            To(this (IRectangle @this, MVector pos) v, MVector scaleToPt) 
            => v.pos.DirectTo(v.@this.Point(scaleToPt));
    }
}
