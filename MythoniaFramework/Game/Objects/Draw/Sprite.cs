using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public class Sprite : SpriteBase
    {

        /// <summary>原点关联的坐标</summary>
        public MPosition OriginPos { get; private set; }
        /// <summary>原点的位置(原点相对于贴图向量)</summary>
        public MVector? OriginDP { get; set; }
        public MAngle Rotation { get; set; }

        public bool IsGameObject { get; set; }


        public Sprite(MGame game, string name, TextureBase texture, MPosition originPos = null, MVector? originDP = null, bool isGameObject = true, MVector? scale = null, MAngle? rotation = null)
            : base(game, name, texture, scale)
        {
            OriginPos = originPos ?? new(0);
            OriginDP = originDP;
            Rotation = rotation ?? new(0);
            IsGameObject = isGameObject;
        }

        public override void Draw(Camera cam, SpriteBatch spriteBatch, float layer, MVector? position = null) => Draw(cam, spriteBatch, 0, layer, position);

        public void Draw(Camera cam, SpriteBatch spriteBatch, int frameNo, float layer, MVector? position)//, MAngle rotation,  float layer)
        {
            base.Draw(spriteBatch, frameNo, layer,
                //IsScreenPosition ? OriginPos : cam.ToScreenPos(OriginPos), //Position
                position is MVector pos ? cam.ToTopLeftPos(pos) : cam.ToScreenPos(OriginPos),
                OriginDP ?? Texture.Origin, //Origin
                IsGameObject ? Scale * cam.Scale : Scale,
                Rotation + ((FlipStatus is Flip.XY) ? 180 : 0));
        }
    }
}
