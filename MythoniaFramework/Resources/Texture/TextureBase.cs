using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public enum Flip { N, X, Y, XY }

    public static class EFlip
    {
        public static SpriteEffects AsDrawEffect(this Flip v) => v switch
        {
            Flip.N or Flip.XY => SpriteEffects.None,
            Flip.X => SpriteEffects.FlipVertically,
            Flip.Y => SpriteEffects.FlipHorizontally,
            _ => throw new IndexOutOfRangeException($"Class Flip only contains 4 value, but it's {v} now")
        };
    }

    public abstract class TextureBase : IMClass
    {
        //---------- Implement - IMClass ----------

        private string _name = null;
        public string Name => _name;
        
        private MGame _game = null;
        public MGame MGame => _game;

        //----------------------------------------



        //--------------- Props ---------------

        public string ImagePath { get; set; }


        /// <summary>单帧贴图的尺寸</summary>
        public MVector FrameSize { get; set; }

        /// <summary>贴图基础放大率</summary>
        public MVector TextureBasicScale { get; set; } = new(1);

        private MVector _origin = (0, 0);
        /// <summary>以 (0, 0) 作为中心点的, 原点位置相对比例<br/>Json自动读取, 在<see cref="ProcessData()"/>中转化成Origin</summary>
        public MVector OriginScale
        {
            get => _origin / FrameSize * 2;
            set => _origin = value / 2 * FrameSize;
        }
        /// <summary>以 (0, 0) 作为中心点, 以像素为单位的原点相对坐标</summary>
        public MVector Origin
        {
            get => _origin;
            set => _origin = value;
        }
        public MVector DrawOrigin => Origin.ChangeSignY() + FrameSize / 2;

        private Texture2D _texture = null;
        public Texture2D Texture => _texture;

        /*void a()
        {
            O*riginScale = 9;
        }*/

        public TextureBase() { }
        public TextureBase(string name) => _name = name;


        /// <summary>
        /// 用于初始化资源<list type="number">
        /// <item>绑定 <see cref="MGame"/> 字段,</item>
        /// <item>绑定 (<seealso cref="Texture2D"/>) <see cref="Texture"/>资源</item>
        /// </list>
        /// <para>
        /// <b>调用:</b><br/><i>
        /// 在 <seealso cref="MGame.LoadContent()"/> <br/>
        /// 调用 <seealso cref="MContentsManager.LoadTextures(string[], string)"/> 加载资源时, <br/>
        /// 在读取<seealso cref="TextureBase"/>资源对象后被调用
        /// </i></para>
        /// <para>
        /// <b>继承规则:</b><br/><i>
        /// 需在开头加上 <see langword="base"/>.LoadInitialize(..)
        /// </i></para>
        /// </summary>
        /// <param name="texture"></param>
        /// <param name=""></param>
        public virtual void LoadInitialize(MGame game, Texture2D texture)
        {
            _game = game;
            _texture = texture;
        }

        /// <summary>
        /// 用于在<see cref="ContentTypeReader"/>中初始化资源<list type="number">
        /// <item>绑定 <see cref="Name"/> 字段,</item></list>
        /// <para>
        /// <b>调用:</b><br/><i>
        /// 在 <see cref="ContentManager.Load{T}(string)"/> 管道 <br/>
        /// (<seealso cref="TextureReader"/>), <br/>
        /// 读取 <seealso cref="TextureBase"/> 资源对象时被调用
        /// </i></para>
        /// </summary>
        /// <param name="game"></param>
        /// <param name="texture"></param>
        public virtual void ReadInitialize(string name)
        {
            _name = name;
        }

        /// <summary>
        /// 在ContantProcessor中被调用, 初始化动画的FramesNo数据
        /// </summary>
        /// <returns>返回自身</returns>
        public virtual TextureBase ProcessData()
        {
            return this;
        }



        /// <summary>获取贴图资源</summary>
        public virtual Texture2D SourceTexture => Texture;



        /*public void DrawTexture(SpriteBatch spriteBatch, int frameNo, float layer, MVector position, MVector scale, MAngle rotation, Color color, Flip flip = Flip.N)
        {

            spriteBatch.Draw(
                SourceTexture,
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
        }*/
    }
}
