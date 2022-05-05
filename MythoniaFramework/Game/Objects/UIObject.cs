using System;
using System.Collections.Generic;
using System.Text;


namespace Mythonia.Framework.Game.Objects
{
    public class UIObject : MObject, Draw.IDrawModule
    {
        /// <summary>
        /// UI对齐窗口的位置 (中心为0 y朝上 相对尺寸坐标)<br/>
        /// default: (-1, 1) 即 左上角
        /// </summary>
        protected MVector Align = VecDir.TopLeft;
        /// <summary>
        /// UI Origin相对Align的偏移 (y朝上 绝对坐标)<br/>
        /// default: (20, 10) * align.GetSign;
        /// </summary>
        protected MVector Offset = new(20, 10);
        /// <summary>
        /// UI的Origin相对 贴图中心点位置 (中心为0 y朝上 相对尺寸坐标)<br/>
        /// default: (-1, 1) 即 贴图左上角
        /// </summary>
        protected MVector Origin = new (-1, 1);
        protected MVector ScreenPosition => Align * MGame.GraphicsDevice.Viewport.Size() / 2
                + Offset - Origin * SpriteObject.TextureSize / 2;



        public Draw.Sprite SpriteObject { get; set; }

        
        public UIObject(MGame game, string name,
            MVector? align = null, MVector? offset = null, MVector? origin = null) :base(game, name)
        {
            if (align  is MVector  align2) Align  =  align2;
            if (offset is MVector offset2) Offset = offset2;
            Offset *= -(MVector)Align.GetSign;
            if (origin is MVector origin2) Origin = origin2;
            else Origin = Align.GetSign;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateBefore(gameTime);
            UpdateAfter(gameTime);
        }

        protected override void UpdateAfter(GameTime gameTime)
        {
            SpriteObject.UpdateSprite(gameTime);
            base.UpdateAfter(gameTime);
        }



        public override string ToString() => $"UIObject \"{Name}\"";

    }
}
