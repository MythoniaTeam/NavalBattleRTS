using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Shapes
{
    internal interface IRectRelativePos : IRectangle
    {
        public MVector OriginScale { get; }

        public sealed MVector OriginDisplacement => Size * OriginScale;

    }
}
