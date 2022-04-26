using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public abstract class TextureBase
    {
        /// <summary>单帧贴图的尺寸</summary>
        public MVector Size { get; protected set; }

        protected Texture2D _texture { get; set; }

        /// <summary>获取需要绘制的帧, 在贴图资源中的范围</summary>
        /// <param name="frameNo">需要绘制的帧编号</param>
        /// <returns></returns>
        public abstract Rectangle GetSourceRange(int frameNo);

        /// <summary>获取贴图资源</summary>
        public virtual Texture2D GetSourceTexture() => _texture;
    }
}
