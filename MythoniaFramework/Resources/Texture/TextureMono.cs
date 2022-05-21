using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class TextureMono : TextureBase, ITexture
    {
        public TextureMono() { }
        public TextureMono(string name) : base(name) { }

        /// <summary>
        /// <para>
        /// 会自动将 <see cref="TextureBase.FrameSize"/> 设为 <see cref="Texture"/> 的尺寸
        /// </para>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        public override void LoadInitialize(MGame game, Texture2D texture)
        {
            base.LoadInitialize(game, texture);
            FrameSize = Texture.Size();
        }



        //---------- Implement - ITexture ----------

        Texture2D ITexture.SourceTexture => Texture;
        MVector ITexture.TextureOrigin => DrawOrigin;
        public Rectangle SourceRange => new(new(0), FrameSize);
    }
}
