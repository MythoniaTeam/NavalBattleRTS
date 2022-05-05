
using TRead = Mythonia.Resources.Texture.TextureBase;


namespace Mythonia.Resources.Texture
{
    public class TextureReader<T> : ContentTypeReader<TRead> where T : TRead, new()
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            T texture = new();
            texture.Name = input.ReadString();
            if(input.ReadBoolean())
                texture.ImagePath = input.ReadString();
            texture.Origin = input.ReadVector2();
            texture.FrameSize = input.ReadVector2();

            return texture;
        }
    }
}
