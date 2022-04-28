using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

using TInput = System.String;

using TOutput = Mythonia.Resources.Texture.TextureMono;

namespace MythoniaPipelineExtension
{
    [ContentProcessor(DisplayName = "TextureMono Processor")]
    class TextureMonoProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {

            return JsonConvert.DeserializeObject<TOutput>(input);
            
        }
    }
}
