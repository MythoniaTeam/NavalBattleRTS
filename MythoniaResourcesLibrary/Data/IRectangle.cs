using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{

    /// <summary>
    /// 包含 矩形属性 的基接口
    /// </summary>
    /// <typeparam name="T">将自身的类型填进去</typeparam>
    public interface IRectangle : IShape, IInterfacenizable<IRectangle>
    {
        public float Width { get; set; }
        public float Height { get; set; }

        /// <summary>矩形的尺寸</summary>
        public MVector Size { get; set; }



        /// <summary>(-X, +Y) 左上角的坐标</summary>
        public MVector TopLeft => new(Left, Top);
        /// <summary>(+X, +Y) 右上角的坐标</summary>
        public MVector TopRight => new(Right, Top);
        /// <summary>(-X, -Y) 左下角的坐标</summary>
        public MVector BottomLeft => new(Left, Bottom);
        /// <summary>(+X, -Y) 右下角的坐标</summary>
        public MVector BottomRight => new(Right, Bottom);

        public float Right => Position.X + Size.X / 2;
        public float Left => Position.X - Size.X / 2;
        public float Top => Position.Y + Size.Y / 2;
        public float Bottom => Position.Y - Size.Y / 2;


        IList<MVector> IShape.Points => new MVector[]{ TopLeft, TopRight, BottomRight, BottomLeft };

    }

}
