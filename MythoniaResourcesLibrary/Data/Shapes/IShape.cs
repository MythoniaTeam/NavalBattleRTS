using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data.Shapes
{
    /// <summary>
    /// 图形的基接口
    /// </summary>
    public interface IShape : IPoint
    {
        public IList<MVector> Points { get; }
    }
}


