using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{
    public class SpriteObject : INamed
    {
        protected List<SubSpriteStructure> SubSprites;

        protected struct SubSpriteStructure
        {
            public string Name => Sprite.Name;

            /// <summary>贴图中心与子贴图Origin的距离</summary>
            public MVector ConnectPoint;

            public SpriteObject Sprite;
        }


        public string Name { get; private set; }

        /// <summary>原点的位置(原点相对于贴图的位置向量)</summary>
        public MVector Origin { get; set; }
        public MAngle Rotation { get; set; }



        /// <summary>透明度</summary>
        public float Transparency { get; set; }

        /// <summary>亮度</summary>
        public float Brightness { get; set; }

        /// <summary>缩放率</summary>
        public float Scale { get; set; }



        public void Draw(MVector originPos, MAngle rotation)
        {
            foreach(SubSpriteStructure subSprite in SubSprites)
            {

                Draw
            }
        }
    }
}
