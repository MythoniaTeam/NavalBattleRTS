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

        public List<Animation> Animations = new List<Animation>();


        public TextureSet() { }
        public TextureSet(string name) : base(name) { }


        public override void InitializeTexture(Texture2D texture)
        {
            Texture = texture;
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
