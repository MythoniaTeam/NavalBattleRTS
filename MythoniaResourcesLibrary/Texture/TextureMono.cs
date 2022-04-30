using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class TextureMono : TextureBase
    {
        public TextureMono() { }
        public TextureMono(string name) : base(name) { }

        public override void InitializeTexture(Texture2D texture)
        {
            Texture = texture;
            FrameSize = Texture.Size();
        }

        public override Rectangle GetSourceRange(int frameNo) => new(new(0), FrameSize);
    }
}
