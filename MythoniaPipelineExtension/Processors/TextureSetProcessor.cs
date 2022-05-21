using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

using TInput = System.String;

using TOutput = Mythonia.Resources.Texture.TextureSet;

namespace MythoniaPipelineExtension
{
    [ContentProcessor(DisplayName = "TextureSet Processor")]
    class TextureSetProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {

            return (TOutput)JsonConvert.DeserializeObject<TOutput>(input).ProcessData();
            
        }
    }
}
