using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class Animation : INamed
    {
        public string Name { get; set; }
        public int[] FramesNo { get; set; }
        public int? FrameNo { get; set; }
        public int?[] FramesRange { get; set; }

        public int FrameCount { get; set; }
        
        public float FrameDuration { get; set; }
        public float CycleDuration => FrameDuration * FrameCount;



        public void Initialize()
        {
            
            if (FramesNo is not null);
            //如果有FramesNo, 跳出循环
            else if (FramesRange is not null)
            {
                if (FramesRange.Length != 2) throw new Exception($"Texture Json Exception, FrameRange should contains 2 int values but not {FramesRange.Length}");
                int min = FramesRange[0] ?? 0;
                int max = FramesRange[1] ?? (FrameCount - 1);

                FramesNo = new int[max - min + 1];
                for (int i = min; i <= max; i++)
                    FramesNo[i] = i;
                
            }
            //有FramesRange
            else if (FrameNo is not null) FramesNo = new int[] { FrameNo ?? 0 };
            //有FrameNo
            else throw new Exception($"Both FramesNo, FrameNo, FramesRange are null, in Animation \"{Name}\"");
            FrameCount = FramesNo.Length;
        }


        public Animation(string name)
        {
            Name = name;
        }

     }
}
