using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class TextureMono : TextureBase
    {
        public override Rectangle GetSourceRange(int frameNo) => new(new(0), Size);
    }
}
