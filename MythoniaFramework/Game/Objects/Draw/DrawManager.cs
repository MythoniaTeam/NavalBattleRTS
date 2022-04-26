using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Draw
{
    public class DrawManager
    {
        private List<TexturePainterBase> _managers = new();


        public void ActionOnManagers(Action<TexturePainterBase> action)
            => ActionOnManagers(action, _managers);

        public void ActionOnManagers(Action<TexturePainterBase> action, Func<TexturePainterBase, bool> selector)
            => ActionOnManagers(action, _managers.Where(selector));

        private void ActionOnManagers(Action<TexturePainterBase> action, List<TexturePainterBase> managers)
        {
            foreach(TexturePainterBase manager in managers)
            {
                action(manager);
            }
        }


        public void Draw(SpriteBatch spriteBatch)

    }

    public static class EList
    {
        /// <summary>
        /// 根据给定Selector, 选择集合中的元素返回
        /// </summary>
        /// <param name="painters"></param>
        /// <param name="selector">集合的选择函数, 给定一个T类型对象, 判定是否符合选择条件, return true表示符合</param>
        /// <returns>集合中, 选择后的对象组成的新集合</returns>
        public static CollType Where<CollType, ItemType>(this CollType items, Func<ItemType, bool> selector)
            where CollType : ICollection<ItemType>, new()
        {
            CollType itemsSelected = new CollType();
            foreach (ItemType item in items)
            {
                if (selector(item)) itemsSelected.Add(item);
            }
            return itemsSelected;
        }
    }
}
