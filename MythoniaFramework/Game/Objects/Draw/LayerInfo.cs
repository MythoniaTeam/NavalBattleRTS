using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    /// <summary>
    /// 所属图层的信息
    /// </summary>
    public struct LayerInfo
    {
        private string _name;

        /// <summary>
        /// 所属图层的路径
        /// </summary>
        public string Path { get; set; }

        public string FullPath => Path is "" or null ? _name : Path + '.' + _name;


        /// <summary>
        /// 所属图层中的权值, 越大表示绘制在越上层
        /// </summary>
        public float Weight;

        //public LayerManager Manager;

        public LayerInfo(string path, float weight, string name)
        {
            _name = name;
            Path = path;
            Weight = weight;
        }

        public override string ToString() => $"path = {FullPath}";
    }
}
