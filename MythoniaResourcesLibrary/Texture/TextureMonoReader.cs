
using TRead = Mythonia.Resources.Texture.TextureBase;

namespace Mythonia.Resources.Texture
{
    public class TextureMonoReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            TextureMono textureBase = new(input.ReadString());
            textureBase.ImagePath = input.ReadString();

            return textureBase;
        }
    }
}
