using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TWrite = Mythonia.Resources.Texture.TextureMono;

namespace Mythonia.Pipeline.Writers
{
    [ContentTypeWriter]
    public class TextureMonoWriter : TextureWriter<TWrite>
    {
        protected override void Write(ContentWriter output, TWrite value)
        {
            base.Write(output, value);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Mythonia.Resources.Texture.TextureMonoReader, Mythonia.Resources";
        }
    }
}
