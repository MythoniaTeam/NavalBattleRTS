using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace Mythonia.Pipeline.Writers
{
    public abstract class TextureWriter<T> : ContentTypeWriter<T> where T : Resources.Texture.TextureBase
    {
        protected override void Write(ContentWriter output, T value)
        {
            output.Write(value.Name);
            output.Write(value.ImagePath is not null);
            if(value.ImagePath is not null)
                output.Write(value.ImagePath);
            output.Write(value.Origin);
            output.Write(value.FrameSize);

        }
    }
}
