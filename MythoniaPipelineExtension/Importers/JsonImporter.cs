using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Graphics;

using TImport = System.String;



namespace MythoniaPipelineExtension
{
    [ContentImporter(".json", DisplayName = "Json Importer", DefaultProcessor = "Processor1")]
    public class JsonImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            return System.IO.File.ReadAllText(filename);
        }
    }
}
