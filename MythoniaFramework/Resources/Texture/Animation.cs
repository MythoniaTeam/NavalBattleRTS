using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Texture
{
    public class Animation : IMClass
    {

        //---------- Implement - IMClass ----------

        private string _name = null;
        public string Name => _name;

        private MGame _game = null;
        public MGame MGame => _game;

        //----------------------------------------



        //--------------- Props ---------------

        public int[] FramesNo { get; set; }
        public int? FrameNo { get; set; }
        public int?[] FramesRange { get; set; }

        public int FrameCount { get; set; }
        
        public float FrameDuration { get; set; }
        public float CycleDuration => FrameDuration * FrameCount;



        //--------------- Constructor ---------------

        public Animation(string name) { _name = name; }



        //--------------- Methods ---------------

        /// <summary>
        /// 用于初始化资源<list type="number">
        /// <item>绑定 <see cref="MGame"/> 字段,</item>
        /// </list>
        /// <para>
        /// <b>调用:</b><br/><i>
        /// 在 <seealso cref="MGame.LoadContent()"/> <br/>
        /// 在<seealso cref="TextureBase.LoadInitialize(MGame, Texture2D)"/> 中遍历被调用
        /// </i></para>
        /// <para>
        /// <b>继承规则:</b><br/><i>
        /// 需在开头加上 <see langword="base"/>.LoadInitialize(..)
        /// </i></para>
        /// </summary>
        /// <param name="texture"></param>
        /// <param name=""></param>
        public void LoadInitialize(MGame game)
        {
            _game = game;
        }

        /// <summary>
        /// <inheritdoc cref="TextureBase.ProcessData"/>
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ProcessData()
        {            
            if (FramesNo is not null) ;
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



    }
}
