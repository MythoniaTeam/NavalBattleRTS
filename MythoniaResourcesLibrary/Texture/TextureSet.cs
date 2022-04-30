using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class TextureSet : TextureBase
    {
        /// <summary>图集每行有多少帧</summary>
        public int FramePerRow { get; set; }
        /// <summary>图集一共有多少帧</summary>
        public int FrameCount { get; set; }

        public string DefaultAnimation { get; set; } = "Normal";
        public Animation[] Animations { get; set; }


        public TextureSet() { }
        public TextureSet(string name) : base(name) { }

        /// <summary>在游戏加载时被调用, 用于绑定Texture2D对象</summary>
        /// <param name="texture"></param>
        public override void InitializeTexture(Texture2D texture)
        {
            Texture = texture;
        }
        ///<summary>在ContantProcessor中被调用, 初始化动画的FramesNo数据</summary>
        ///<returns>自身Texture对象</returns>
        public override TextureBase InitializeData()
        {
            base.InitializeData();
            foreach (Animation ani in Animations) ani.Initialize();
            return this;
        }


        public override Rectangle GetSourceRange(int frameNo)
        {
            if (frameNo >= FrameCount) throw new IndexOutOfRangeException($"TextureSet \"{Name}\" doesn't have frame #{frameNo}, it has only {FrameCount} frames");
            return new Rectangle(
                (frameNo % FramePerRow, frameNo / FramePerRow) * FrameSize,
                FrameSize);
        }

        public Animation GetAnimation(string aniName)
        {
            foreach (Animation animation in Animations)
                if (animation.Name == aniName)
                    return animation;
            throw new ObjectNotFoundException("Animation", aniName);
        }
    }
}
