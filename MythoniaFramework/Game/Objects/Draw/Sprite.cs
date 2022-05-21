using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Objects.Draw
{
    /// <summary>
    /// 用于绘制贴图, 管理相关动态数据的 <see cref="Sprite"/> 对象 (eg: 动态缩放率, 透明度, 旋转等)
    /// </summary>
    public class Sprite : ILayerItem
    {
        //---------- Implement - IMClass ----------

        private readonly string _name;
        public string Name => _name;
        private readonly MGame _game;
        public MGame MGame => _game;

        //----------------------------------------



        //---------- Prop - Texture ----------

        protected ITexture Texture { get; private set; }
        /// <summary>贴图绘制时的大小 (未经CameraTransform)</summary>
        public MVector TextureDrawSize => Texture.SourceTexture.Size() * SpriteScale;



        //---------- Prop - Color ----------

        /// <summary>透明度</summary>
        public float Transparency
        {
            get => _transparency;
            set
            {
                _transparency = value;
                _color = new(_color, value);
            }
        }
        private float _transparency;

        /// <summary>亮度</summary>
        public float Brightness
        {
            get => _brightness;
            set
            {
                _brightness = value;
                _color = new(value, value, value, _transparency);
            }
        }
        private float _brightness;
        /// <summary>绘制时算上亮度和透明度的颜色</summary>
        public Color DrawColor() => _color;
        private Color _color = Color.White;



        //---------- Prop - Scale ----------

        private MVector _scale = new(1);
        /// <summary>绘制时的缩放率 (算上贴图本身BasicScale) (未经CameraTransform)</summary>
        private MVector SpriteScale => _scale * Texture.TextureBasicScale;

        /// <summary>
        /// 获取 <b>转化后缩放率</b> 的方法委托, 
        /// <para>
        /// <b>调用:</b> <br/><i>
        /// 给定 <b><seealso cref="Sprite"/> @this, <seealso cref="Camera"/> cam</b> <br/>
        /// 在函数内 @this.SpriteScale 获取基本缩放率, <br/>
        /// 按照一定规则, 结合cam 将缩放率转化<br/></i>
        /// </para><br/>
        /// <para>
        /// <b>返回:</b> <br/><i>
        /// <b>转换后的, 绘制时的缩放率</b></i>
        /// </para>
        /// <para>
        /// <b>默认:</b> <br/>
        /// <code>(@this, cam) => <br/>
        ///     @this.IsGameObject ? @this.SpriteScale * cam.Scale : @this.SpriteScale * cam.UIScale;
        /// </code>
        /// </para><br/>
        /// </summary>
        private Func<Sprite, Camera, MVector> GetTransformedScale = (@this, cam) => 
            @this.IsGameObject ? @this.SpriteScale * cam.Scale : @this.SpriteScale * cam.UIScale;

        protected MVector DrawScale(Camera cam) => GetTransformedScale(this, cam);

        public void SetScale(float v) => _scale = new(v);
        public void SetScale(float x, float y) => _scale = new(x, y);
        public void SetScale(MVector v) => _scale = v;
        public void SetScaleX(float v) => _scale.X = v;
        public void SetScaleY(float v) => _scale.Y = v;



        //---------- Prop - Flip ----------

        public bool FlipX
        {
            get => _flipStatus is Flip.X or Flip.XY;
            set
            {
                _flipStatus = (value, _flipStatus) switch
                {
                    //翻转x
                    (true, Flip.N or Flip.X) => Flip.X, //status是Flip.N或Flip.X, 表示y没有翻转
                    (true, Flip.Y or Flip.XY) => Flip.XY, //status是Flip.Y或Flip.XY, 表示y翻转了
                    //取消翻转x
                    (false, Flip.N or Flip.X) => Flip.N,  //status是Flip.N或Flip.X, 表示y没有翻转
                    (false, Flip.Y or Flip.XY) => Flip.Y,  //status是Flip.Y或Flip.XY, 表示y翻转了
                    _ => throw new Exception($"FlipStatus Exception occurred when setting FlipX = {value}, the range of FlipStatus should be [Flip.N, Flip.XY], but it's value is {_flipStatus} now")
                };
            }
        }
        public bool FlipY
        {
            get => _flipStatus is Flip.Y or Flip.XY;
            set
            {
                _flipStatus = (value, _flipStatus) switch
                {
                    //翻转y
                    (true, Flip.N or Flip.Y) => Flip.Y, //status是Flip.N或Flip.X, 表示x没有翻转
                    (true, Flip.X or Flip.XY) => Flip.XY, //status是Flip.Y或Flip.XY, 表示x翻转了
                    //取消翻转y
                    (false, Flip.N or Flip.Y) => Flip.N,  //status是Flip.N或Flip.X, 表示x没有翻转
                    (false, Flip.X or Flip.XY) => Flip.X,  //status是Flip.Y或Flip.XY, 表示x翻转了
                    _ => throw new Exception($"FlipStatus Exception occurred when setting FlipY = {value}, the range of FlipStatus should be [0, 3], but it's value is {_flipStatus} now")
                };
            }
        }
        private Flip _flipStatus = 0;
        protected Flip FlipStatus => _flipStatus;



        //---------- Prop - Position ----------

        ///// <summary>绘制在屏幕上的坐标</summary>
        //protected MVector GetDrawPos(Camera cam) => OriginToScreenPos(GetOriginPos(), cam);
        /// <summary>
        /// 获取Origin坐标的算法委托, 在外部定义用于传递变量, <br/> 在构造函数中被赋值
        /// <para>
        /// <b>调用:</b> <br/><i>
        /// <b>不应直接调用该委托方法,</b>
        /// 仅在 <see cref="GetTransformedPos"/>中 被调用, <br/>
        /// 该方法会进一步, 将本方法的返回值转换成屏幕上的坐标</i>
        /// </para><br/>
        /// <para>
        /// <b>返回:</b> <br/><i>
        /// <b>原点的坐标</b><br/>
        /// (可能是UI的相对屏幕中心坐标, 或游戏对象的世界坐标) <br/> 
        /// (需在 <see cref="GetTransformedPos"/>中 被进一步转化)
        /// </i></para>
        /// <para>
        /// <b>示例:</b> <br/>
        /// 如下所示, 可传递 TestObject 的 TestObjectPos 属性
        /// <code>
        /// <see langword="class"/> TestObject : <see cref="GameObject"/>
        /// {                                                                                                                   
        ///     <see langword="private"/> <see cref="MVector"/> TestObjectPos;                                                  
        ///     <see langword="public"/> TestObject(..)                                                                         
        ///     {                                                                                                               
        ///         SpriteObject = <see langword="new"/> (.. , getOriginPosMethod: () => <see langword="this"/>.TestObjectPos); 
        ///     }                                                                                                               
        /// }                                                                                                                   
        /// </code>
        /// </para>
        /// </summary>
        private Func<MVector> GetOriginPos;

        /// <summary>
        /// 获取 <b>转化后坐标</b> 的方法委托, 
        /// <para>
        /// <b>调用:</b> <br/><i>
        /// 给定 <b><seealso cref="Sprite"/> @this, <seealso cref="Camera"/> cam</b> <br/>
        /// 在函数内 调用@this.GetOriginPos() 获取原点坐标, <br/>
        /// 按照一定规则, 结合cam 将坐标转化<br/></i>
        /// </para><br/>
        /// <para>
        /// <b>返回:</b> <br/><i>
        /// <b>转换后的, 屏幕上的绘制坐标</b></i>
        /// </para>
        /// <para>
        /// <b>默认:</b> <br/>
        /// <code>(@this, cam) => <br/>
        ///     @this.IsWorldPos? cam.ToScreenPos(@this.GetOriginPos()) : cam.ToTopLeftPos(@this.GetOriginPos());
        /// </code>
        /// </para><br/>
        /// </summary>
        private Func<Sprite, Camera, MVector> GetTransformedPos = (@this, cam) =>
            @this.IsWorldPos ? cam.ToScreenPos(@this.GetOriginPos()) : cam.ToTopLeftPos(@this.GetOriginPos());

        /// <summary>绘制在屏幕上的坐标, 自动调用 <see cref="GetTransformedPos"/> 委托方法</summary>
        /// <param name="cam">转换坐标用的 <seealso cref="Camera"/> 对象</param>
        /// <returns>转换后的坐标</returns>
        protected MVector DrawPos(Camera cam) => GetTransformedPos(this, cam);


        //// <summary>原点的位置(原点相对于贴图向量)</summary>
        //public MVector? OriginDP { get; set; }
        /// <summary>
        /// 是世界坐标吗?<list type="table">
        /// <item><term><see langword="true"/></term><description><i>对象会 随<seealso cref="Camera"/> 改变绘制在屏幕上的坐标 <b>(游戏对象, 少部分UI)</b></i></description></item>
        /// <item><term><see langword="false"/></term><description><i>对象是 <b>固定在屏幕上</b> 的UI元素 <b>(大部分UI)</b></i></description></item>
        /// </list>
        /// </summary>
        protected bool IsWorldPos { get; set; }
        /// <summary>
        /// 是游戏对象吗吗?<list type="table">
        /// <item><term><see langword="null"/></term><description><i><b>(default)</b> = <paramref name="isWorldPos"/></i></description></item>
        /// <item><term><see langword="true"/></term><description><i>对象是 游戏对象, 会 随<seealso cref="Camera"/>缩放</i></description></item>
        /// <item><term><see langword="false"/></term><description><i>对象是 UI元素, 不会 随<seealso cref="Camera"/>缩放</i></description></item>
        /// </list>
        /// </summary>
        protected bool IsGameObject { get; set; }



        //---------- Prop - Rotation ----------

        public MAngle DrawRotation() => FlipStatus is Flip.XY ? Rotation + 180 : Rotation;
        public MAngle Rotation { get; set; }



        //--------------- Constructor ---------------


        private Sprite(MGame game, string name) { _game = game; _name = name; }

        /// <summary>
        /// 生成一个Sprite贴图对象, 用于绘制自己
        /// </summary>
        /// <param name="game">所属的 <see cref="MGame"/> 游戏对象</param>
        /// <param name="name">对象的名称</param>
        /// <param name="texture">贴图对象</param>
        /// <param name="getOriginPosMethod">
        /// 获取Origin坐标的算法
        /// <para>
        /// <b>参见:</b> <i><seealso cref="GetOriginPos"/></i>
        /// </para>
        /// </param>
        /// <param name="isWorldPos">
        /// 是相对 <see cref="Camera"/> 的 世界坐标 吗?
        /// <para><b>参见:</b> <i><seealso cref="IsWorldPos"/></i></para>
        /// </param>
        /// <param name="isGameObject">
        /// 是游戏对象吗吗?<list type="table">
        /// <item><term><see langword="null"/> (default)</term><description><i> 设为 <paramref name="isWorldPos"/> 的值</i></description></item>
        /// </list>
        /// <para><b>参见:</b> <i><seealso cref="IsGameObject"/></i></para>
        /// </param>
        /// <param name="layerInfo"> 图层的数据</param>
        /// <param name="scale">初始的缩放比例</param>
        /// <param name="rotation">初始的旋转角度</param>
        /// <param name="getTransformedPosMethod">
        /// 获取 <b>转化后坐标</b> 的方法委托, 将<paramref name="getOriginPosMethod"/> 获取后的坐标进行转化
        /// <para><b>参见:</b> <i><seealso cref="GetTransformedPos"/></i></para>
        /// </param>
        public Sprite(MGame game, string name, ITexture texture,
            Func<MVector> getOriginPosMethod, /*MVector? originDP,*/ 
            bool isWorldPos = true, bool? isGameObject = null, LayerInfo? layerInfo = null,
            MVector? scale = null, MAngle? rotation = null,
            Func<Sprite, Camera, MVector> getTransformedPosMethod = null, Func<Sprite, Camera, MVector> getTransformedScaleMethod = null
            ) : this(game, name)
        { 

            Texture = texture; 

            LayerInfo = layerInfo ?? game._GetDefaultLayerInfo(name);
            game.DrawManager.Layers.AddItem(this);

            SetScale(scale ?? new(1));
            Rotation = rotation ?? new(0);

            IsWorldPos = isWorldPos;
            IsGameObject = isGameObject ?? !isWorldPos;

            GetOriginPos = getOriginPosMethod;
            if (getTransformedPosMethod is not null) GetTransformedPos = getTransformedPosMethod;
            if (getTransformedScaleMethod is not null) GetTransformedScale = getTransformedScaleMethod;


        }



        //--------------- Methods ---------------

        public void DrawSprite(Camera cam, SpriteBatch spriteBatch, float layer)
        {
            spriteBatch.Draw(
                Texture.SourceTexture,
                DrawPos(cam),
                Texture.SourceRange,
                DrawColor(),
                DrawRotation(),
                Texture.TextureOrigin,
                DrawScale(cam),
                FlipStatus.AsDrawEffect(),
                layer
                );

        }


        /// <summary>
        /// 每帧执行的
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void UpdateSprite(GameTime gameTime) { }



        //---------- Override Methods ----------

        public override string ToString() => $"\"{Name}\" at layer: {LayerInfo}";



        //---------- Implement - ILayerItem ----------

        public LayerInfo LayerInfo { get; set; }
        public int ItemsCount() => 1;
        public ICollection<Sprite> GetLayerSprites() => new Sprite[] { this };


        
    }
}
