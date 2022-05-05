using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public class Sprite : ILayerItem
    {

        public MGame MGame;
        public string Name { get; private set; }


        protected TextureBase Texture;
        public MVector TextureSize => Texture.GetSourceTexture().Size() * Scale;


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
        public Color Color => _color;
        private Color _color = Color.White;



        private MVector _scale = new(1);
        /// <summary>缩放率</summary>
        public MVector Scale => _scale;
        public void SetScale(float v) => _scale = new(v);
        public void SetScale(float x, float y) => _scale = new(x, y);
        public void SetScale(MVector v) => _scale = v;

        public void SetScaleX(float v) => _scale.X = v;
        public void SetScaleY(float v) => _scale.Y = v;


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



        /// <summary>原点关联的坐标</summary>
        protected MPosition OriginPos { get; private set; }
        //// <summary>原点的位置(原点相对于贴图向量)</summary>
        //public MVector? OriginDP { get; set; }
        public MAngle Rotation { get; set; }

        public bool IsWorldPos { get; set; }

        /// <summary>获取坐标的方式, 默认是直接返回字段OriginPos</summary>
        public Func<MVector> GetOriginPos;




        private Sprite(MGame game, string name, TextureBase texture, 
            /*MVector? originDP,*/ bool isWorldPos, LayerInfo? layerInfo,
            MVector? scale, MAngle? rotation)
        {
            MGame = game;
            Name = name;

            Texture = texture;

            //OriginDP = originDP;
            IsWorldPos = isWorldPos;

            LayerInfo = layerInfo ?? game._GetDefaultLayerInfo(name);
            game.DrawManager.Layers.AddItem(this);

            SetScale(scale ?? new(1));
            Rotation = rotation ?? new(0);
        }
        public Sprite(MGame game, string name, TextureBase texture,
            MPosition originPos, /*MVector? originDP = null,*/ bool isWorldPos = true, LayerInfo? layerInfo = null,
            MVector? scale = null, MAngle? rotation = null)
            : this(game, name, texture,/* originDP,*/ isWorldPos, layerInfo, scale, rotation)
        {
            OriginPos = originPos ?? new(0);
            GetOriginPos = () => OriginPos;
        }

        public Sprite(MGame game, string name, TextureBase texture,
            Func<MVector> getOriginPosMethod,/* MVector? originDP = null,*/ bool isWorldPos = true, LayerInfo? layerInfo = null,
            MVector? scale = null, MAngle? rotation = null)
            : this(game, name, texture,/* originDP,*/ isWorldPos, layerInfo, scale, rotation)
        {
            GetOriginPos = getOriginPosMethod;
        }

        public virtual void DrawSprite(Camera cam, SpriteBatch spriteBatch, float layer) => DrawFrame(cam, spriteBatch, 0, layer);

        protected void DrawFrame(Camera cam, SpriteBatch spriteBatch, int frameNo, float layer)//, MAngle rotation,  float layer)
        {
            Texture.DrawTexture(spriteBatch, frameNo, layer,
                !IsWorldPos ? cam.ToTopLeftPos(GetOriginPos()) : cam.ToScreenPos(GetOriginPos()),
                Scale * cam.Scale, Rotation, Color, FlipStatus);
        }


        public virtual void UpdateSprite(GameTime gameTime) { }



        public override string ToString() => $"\"{Name}\" at layer: {LayerInfo}";


        //---------- Implement - ILayerItem ----------

        public LayerInfo LayerInfo { get; set; }
        public int ItemsCount() => 1;
        public ICollection<Sprite> GetLayerSprites() => new Sprite[] { this };


        
    }
}
