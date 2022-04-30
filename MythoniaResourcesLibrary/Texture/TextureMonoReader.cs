
using TRead = Mythonia.Resources.Texture.TextureBase;

namespace Mythonia.Resources.Texture
{
    public class TextureMonoReader : TextureReader<TextureMono>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            TextureMono textureBase = (TextureMono)base.Read(input, existingInstance);

            return textureBase;
        }
    }
}
