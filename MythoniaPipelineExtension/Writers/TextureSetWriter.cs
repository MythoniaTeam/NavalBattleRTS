using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using Mythonia.Resources.Texture;

using TWrite = Mythonia.Resources.Texture.TextureSet;

namespace Mythonia.Pipeline.Writers
{
    [ContentTypeWriter]
    internal class TextureSetWriter : ContentTypeWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            output.Write(value.Name);
            output.Write(value.ImagePath);

            output.Write(value.FramePerRow);
            output.Write(value.FrameCount);

            output.Write(value.FrameSize);

            
            output.Write(value.Animations.Count);
            foreach (Animation ani in value.Animations)
            {
                output.Write(ani.Name);
                output.Write(ani.FrameDuration);
                output.Write(ani.FrameCount);
                foreach(int no in ani.FramesNo)
                    output.Write(no);
            }




        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Mythonia.Resources.Texture.TextureMonoReader, TextureMonoReader";
        }
    }
}
