using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public enum Flip { N, X, Y, XY }
    public abstract class TextureBase : INamed
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }

        public Vector2 A { get; set; }

        /// <summary>单帧贴图的尺寸</summary>
        public MVector FrameSize { get; set; }

        public MVector BasicScale { get; set; } = new(1);

        private MVector _origin = new(0, 0);
        private bool _origin_invalid = true;
        public MVector OriginScale { get; set; }
        public MVector Origin
        {
            get
            {
                if (_origin_invalid)
                {
                    _origin = (OriginScale + 1) / 2 * FrameSize;
                    _origin_invalid = false;
                }
                return _origin;
            }
            set => _origin = value;
        }
        protected Texture2D Texture { get; set; }

        /*void a()
        {
            O*riginScale = 9;
        }*/

        public TextureBase() { }
        public TextureBase(string name) => Name = name;


        public abstract void InitializeTexture(Texture2D texture);

        public virtual TextureBase InitializeData()
        {
            return this;
        }


        /// <summary>获取需要绘制的帧, 在贴图资源中的范围</summary>
        /// <param name="frameNo">需要绘制的帧编号</param>
        /// <returns></returns>
        public abstract Rectangle GetSourceRange(int frameNo);

        /// <summary>获取贴图资源</summary>
        public virtual Texture2D GetSourceTexture() => Texture;



        public void DrawTexture(SpriteBatch spriteBatch, int frameNo, float layer, MVector position, MVector scale, MAngle rotation, Color color, Flip flip = Flip.N)
        {

            spriteBatch.Draw(
                GetSourceTexture(),
                position,
                GetSourceRange(frameNo),
                color,
                rotation + ((flip is Flip.XY) ? 180 : 0),
                Origin,
                scale * BasicScale,
                flip switch
                {
                    Flip.X => SpriteEffects.FlipVertically,
                    Flip.Y => SpriteEffects.FlipHorizontally,
                    _ => SpriteEffects.None
                },
                layer
            );
        }
    }
}
