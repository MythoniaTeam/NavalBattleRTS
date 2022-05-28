
using TRead = Mythonia.Resources.Texture.TextureBase;

namespace Mythonia.Resources.Texture
{
    public class TextureSetReader : TextureReader<TextureSet>
    {
        protected override TRead Read(ContentReader input, TRead existingInstance)
        {
            TextureSet texture = (TextureSet)base.Read(input, existingInstance);

            texture.FramePerRow = input.ReadInt32();
            texture.FrameCount = input.ReadInt32();


            texture.DefaultAnimation = input.ReadString();

            int aniCount = input.ReadInt32();
            Animation ani;
            texture.Animations = new Animation[aniCount];
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
                texture.Animations[i] = ani;
                ani.FrameCount = frameCount;
            }


            return texture;
        }
    }
}
