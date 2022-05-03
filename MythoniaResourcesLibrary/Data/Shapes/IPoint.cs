using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data.Shapes
{
    public interface IPoint
    {
        /// <summary>z中心点的坐标</summary>
        public IVector Position { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
