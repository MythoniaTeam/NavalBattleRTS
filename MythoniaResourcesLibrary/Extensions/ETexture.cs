﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Extensions
{
    public static class ETexture
    {
        public static MVector Size(this Texture2D v) => new(v.Width, v.Height);
    }
}
