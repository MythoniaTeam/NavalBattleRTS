using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public class Sprite 
    {
        /*protected List<SubSprite> SubSprites;

        public class SubSprite
        {
            public string Name => Sprite.Name;

            /// <summary>贴图中心与子贴图Origin的距离</summary>
            private MPosition ConnectPoint;
            public void SetConnectPoint(MAngle rotation, MVector originPos, MVector originDP)
            {
                ConnectPoint = rotation.Rotate(originPos - originDP + ConnectPointDP);
            }


            public MVector ConnectPointDP { get; private set; }

            public SpriteObject Sprite;

            public SubSprite(SpriteObject sprite, MVector connectPointDP)
            {
                Sprite = sprite;
                ConnectPointDP = connectPointDP;
                ConnectPoint = sprite.OriginPos;
            }
        }*/

        


        public string Name { get; private set; }
        protected TextureBase Texture;
        protected MVector TextureSize;



        /// <summary>原点关联的坐标</summary>
        public MPosition OriginPos { get; private set; }
        /// <summary>原点的位置(原点相对于贴图的位置向量)</summary>
        public MVector OriginDP { get; set; }
        public MAngle Rotation { get; set; }



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
        protected enum Flip { N, X, Y, XY }
        private Flip _flipStatus = 0;
        protected Flip FlipStatus => _flipStatus;


        public Sprite(string name, TextureBase texture, MPosition originPos = null, MVector? originDP = null, MVector? scale = null, MAngle? rotation = null)
        {
            Name = name;
            OriginPos = originPos ?? new(0);
            OriginDP = originDP ?? new(0);
            SetScale(scale ?? new(1));
            Rotation = rotation ?? new(0);

            Texture = texture;
            TextureSize = new(Texture.GetSourceTexture().Width, Texture.GetSourceTexture().Height);
        }


        /*public SubSprite GetSubSprite(string name)
        {
            foreach (SubSprite subSprite in SubSprites)
            {
                if (subSprite.Name == name) return subSprite;
            }
            throw new ObjectNotFoundException("SubSprite", name);
        }*/

        public virtual void Draw(Camera cam, SpriteBatch spriteBatch, float layer) => Draw(cam, spriteBatch, 0, layer);
        public void Draw(Camera cam, SpriteBatch spriteBatch, int frameNo, float layer)//, MAngle rotation,  float layer)
        {
            MAngle rotation = Rotation;
            //rotation += Rotation;

            spriteBatch.Draw(
                Texture.GetSourceTexture(),
                cam.ToScreenPos(OriginPos),
                Texture.GetSourceRange(frameNo),
                Color,
                rotation + ((FlipStatus is Flip.XY) ? 180 : 0),
                OriginDP + TextureSize / 2,
                Scale * cam.Scale,
                FlipStatus switch
                {
                    Flip.X => SpriteEffects.FlipVertically,
                    Flip.Y => SpriteEffects.FlipHorizontally,
                    _ => SpriteEffects.None
                },
                layer
            );

            /*foreach (SubSprite subSprite in SubSprites)
            {
                subSprite.SetConnectPoint(rotation, OriginPos, OriginDP);
                subSprite.Sprite.Draw(spriteBatch, rotation, frameNo, layer);
            }*/
        }


        public virtual void Update(GameTime gameTime) { }

    }
}
