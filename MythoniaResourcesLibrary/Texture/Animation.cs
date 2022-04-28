using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class Animation : INamed
    {
        public string Name { get; set; }
        public int[] FramesNo { get; set; }
        public int FrameNo
        {
            set => FramesNo = new int[] { value };
        }
        public int?[] FramesRange
        {
            set
            {
                if (value.Length != 2) throw new Exception($"Texture Json Exception, FrameRange should contains 2 int values but not {value.Length}");
                int min = value[0] ?? 0;
                int max = value[1] ?? FrameCount;

                FramesNo = new int[max - min + 1];
                for(int i = min; i <= max; i++)
                    FramesNo[i] = i;

            }
        }

        public int FrameCount => FramesNo.Length;
        
        public float FrameDuration { get; set; }
        public float CycleDuration => FrameDuration * FrameCount;

        public Animation(string name)
        {
            Name = name;
        }

     }
}
