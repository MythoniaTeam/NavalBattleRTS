
using TRead = Mythonia.Resources.Texture.TextureBase;

namespace Mythonia.Resources.Texture
{
    public class TextureSetReader : ContentTypeReader<TRead>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            TextureSet textrue = new(input.ReadString());
            textrue.ImagePath = input.ReadString();

            textrue.FramePerRow = input.ReadInt32();
            textrue.FrameCount = input.ReadInt32();

            textrue.FrameSize = input.ReadVector2();

            int aniCount = input.ReadInt32();
            Animation ani;
            textrue.Animations = new();
            for(int i = 0; i < aniCount; i++)
            {
                ani = new(input.ReadString());
                ani.FrameDuration = input.ReadSingle();
                int frameCount = input.ReadInt32();
                ani.FramesNo = new int[frameCount];
                for(int j = 0; j < frameCount; j++)
                {
                    ani.FramesNo[j] = input.ReadInt32();
                }
            }


            return textrue;
        }
    }
}
