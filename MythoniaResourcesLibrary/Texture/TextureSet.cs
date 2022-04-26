using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class TextureSet : TextureBase
    {
        public string Name { get; private set; }

        /// <summary>图集每行有多少帧</summary>
        private int FrameCountPerRow;
        /// <summary>图集一共有多少帧</summary>
        private int FrameCount;

        private List<Animation> Animations = new List<Animation>();

        public override Rectangle GetSourceRange(int frameNo)
        {
            if (frameNo >= FrameCount) throw new IndexOutOfRangeException($"TextureSet \"{Name}\" doesn't have frame #{frameNo}, it has only {FrameCount} frames");
            return new Rectangle(
                new MVector(frameNo % FrameCountPerRow, frameNo / FrameCountPerRow) * Size,
                Size);
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
