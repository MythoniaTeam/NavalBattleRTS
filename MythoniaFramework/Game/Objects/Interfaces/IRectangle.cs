



namespace Mythonia.Game.Objects.Interfaces
{



    public interface IRectangle : IPosition, ISize
    {
        public IRectangle AsRect { get; }



        #region Implement - IPosition 

        MVector IPosition.Position => CenterPos;

        #endregion



        #region Implement - IPosition 

        MVector ISize.Size => Size;

        //

        #endregion



        #region Props - Position 

        public sealed MVector CenterPos => new(CenterX, CenterY);

        public float CenterX { get; }
        public float CenterY { get; }

        #endregion



        #region Props - Bounds 

        /// <summary>
        /// <see cref="IRectangle"/> 的尺寸,<br/>
        /// 即 (<see cref="Width"/>, <see cref="Height"/>)
        /// </summary>
        public new sealed MVector Size => new(Width, Height);

        /// <summary><see cref="IRectangle"/> 的高度</summary>
        public float Height { get; }
        /// <summary><see cref="IRectangle"/> 的宽度</summary>
        public float Width { get; }

        #endregion



        #region Props - Bounds 

        /// <summary><b>顶部</b> 的Y坐标</summary>
        public float Top => CenterX + Height / 2;

        /// <summary><b>底部</b> 的Y坐标</summary>
        public float Bottom => CenterX - Height / 2;

        /// <summary><b>左侧</b> 的X坐标</summary>
        public float Left  => CenterY - Width / 2;

        /// <summary><b>右侧</b> 的X坐标</summary>
        public float Right => CenterY + Width / 2;

        /*
         * public float Top    => Math.Max(Y1, Y2);
         * public float Bottom => Math.Min(Y1, Y2);
         * public float Left   => Math.Min(X1, X2);
         * public float Right  => Math.Max(X1, X2);
         * 
         * public float X1 => CenterX + Height / 2;
         * public float X2 => CenterX - Height / 2;
         * public float Y1 => CenterY - Width / 2;
         * public float Y2 => CenterY + Width / 2;
         */

        #endregion



        #region Props - Points 

        public sealed MVector PointCenter => new(CenterX, CenterY);
        public sealed MVector PointTop => new(CenterX, Top);
        public sealed MVector PointBottom => new(CenterX, Bottom);
        public sealed MVector PointLeft => new(Left, CenterY);
        public sealed MVector PointRight => new(Right, CenterY);
        public sealed MVector PointTL => new(Left, Top);
        public sealed MVector PointTR => new(Right, Top);
        public sealed MVector PointBL => new(Left, Bottom);
        public sealed MVector PointBR => new(Right, Bottom);

        #endregion



        #region Methods 

        /// <summary>
        /// 给定一个比例, 返回对应的一点的 <b>相对坐标</b>
        /// </summary>
        /// <param name="scale">x, y ∈ [-1, 1], 右上为 + 的比例 (超出该区间, 点将位于Rectangle外)</param>
        /// <returns>该点 <b>相对于Center</b> 的坐标</returns>
        public MVector GetPointRelPos(MVector scale) => Size * scale / 2;

        /// <summary>
        /// 给定一个<see cref="MVector"/>比例, 返回对应一点的 <b>绝对坐标</b>
        /// </summary>
        /// <param name="scale">x, y ∈ [-1, 1], 右上为 + 的比例 (超出该区间, 点将位于Rectangle外)</param>
        /// <returns>该点的 <b>绝对坐标</b></returns>
        public sealed MVector GetPointPos(MVector scale) => GetPointRelPos(scale) + CenterPos;



        /// <summary>
        /// 调用此方法, 返回自身IRectangle对象和scale后的坐标, 配合扩展方法 <see cref="EDirectFrom.To((IRectangle @this, MVector pos), MVector)"/> 使用
        /// <para><b>示例:</b><br/>
        /// <i>下文中的<seealso cref="IRectangle"/> 在实际运用时, 需替换成对应实例. <br/>
        /// (即: <c><see cref="IRectangle"/> rect; rect.DirectFrom(..)..</c>)</i>
        /// <code>
        /// <see cref="IRectangle"/>.DirectFrom(<see cref="PointTR"/>).To(<see cref="PointCenter"/>).For(20, 10)
        /// </code>
        /// <i>这行代码 将获取一个 <seealso cref="MVector"/> 坐标, <br/>
        /// 表示从 <b>右上角</b>(TR), 朝 <b>中心点</b>(Center) 的大概方向, 即<b>左下方</b>, <br/>
        /// (大概方向, 即八个基本方向, 见 <seealso cref="VecDir"/>)<br/>
        /// x移动20单位, y移动10单位<br/>
        /// (也可以省略 To(..), 直接 For(..), 相当于 To(VecDir.Center))
        /// </i></para>
        /// </summary>
        /// <param name="frPtScale">起始点的位置, 以 [-1 ~ 1] 的比例表示 [下~上] / [左~右]</param>
        /// <returns>
        /// <list type="number">
        /// <item>自身 <see cref="IRectangle"/> 对象</item>
        /// <item>一个按比例计算的坐标</item>
        /// </list>
        /// </returns>
        public sealed (IRectangle @this, MVector pos) DirectFrom(MVector frPtScale) => (this, GetPointPos(frPtScale));

        #endregion

    }

    public static class EDirect
    {
        /// <summary>
        /// 配合 <see cref="IRectangle.DirectFrom(MVector)"/>, EDirect.For(..) 使用
        /// </summary>
        /// <param name="v"></param>
        /// <param name="toPtScale">
        /// x, y ∈ [-1, 1], 右上为 + 的比例 (超出该区间, 点将位于Rectangle外)<br/>
        /// <b>建议:</b> <i>使用<see cref="VecDir"/></i>
        /// <para><b>默认:</b><br/><i><see cref="VecDir.Center"/></i></para>
        /// </param>
        /// <returns></returns>
        public static (MVector pos, VecDir dir) To(this (IRectangle @this, MVector pos) v, MVector? toPtScale = null) 
            => v.pos.DirectTo(v.@this.GetPointPos(toPtScale ?? VecDir.Center));

        public static MVector For(this (MVector pos, VecDir dir) v, MVector displacement) => v.pos + v.dir * displacement;
        public static MVector For(this (MVector pos, VecDir dir) v, float dx, float dy) => v.For(new(dx, dy));

        public static MVector For(this (IRectangle @this, MVector pos) v, MVector displacement) => v.To().For(displacement);
        public static MVector For(this (IRectangle @this, MVector pos) v, float dx, float dy) => v.To().For(dx, dy);

    }
}
