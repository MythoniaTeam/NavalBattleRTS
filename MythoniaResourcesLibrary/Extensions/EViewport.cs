using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Extensions
{
    public static class EViewport
    {
        public static MVector Size(this Viewport v) => new(v.Width, v.Height);

    }
}
