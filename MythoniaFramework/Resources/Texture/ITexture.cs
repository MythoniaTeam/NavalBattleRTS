


namespace Mythonia.Resources.Texture
{
    public interface ITexture
    {
        public Texture2D SourceTexture { get; }



        /// <summary>获取需要绘制的帧, 在贴图资源中的范围</summary>
        public Rectangle SourceRange { get; }

        /// <summary>贴图的基础缩放率</summary>
        public MVector TextureBasicScale { get; }

        /// <summary>贴图的基础原点绘制位置</summary>
        public MVector TextureOrigin { get; }


    }
}
