using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public interface ILayerItem : INamed
    {
        /// <summary>
        /// <b>所属图层</b> 的信息
        /// </summary>
        public LayerInfo LayerInfo { get; }

        /// <summary>
        /// 被递归调用, 返回当前 子对象 + 自身 的对象总数
        /// </summary>
        /// <returns></returns>
        public int ItemsCount();

        /// <summary>
        /// 被递归调用, 返回子对象的集合
        /// </summary>
        /// <returns></returns>
        public ICollection<Sprite> GetLayerSprites();
    }
}
