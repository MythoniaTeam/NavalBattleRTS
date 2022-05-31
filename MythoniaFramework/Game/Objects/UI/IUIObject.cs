using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Objects.UI
{
    public interface IUIObject : ITouchCheck
    {
        protected ICollection<IUIObject> SubUIObjects { get; }


        protected bool WasTouch { get; set; }
        protected bool WasClick { get; set; }


        /// <summary>
        /// 调用 <see cref="CheckTouch(MVector)"/> 方法, 并自动按照结果调用 Action 等方法
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="mousePos"></param>
        /// <param name="click"></param>
        public void Update(GameTime gameTime, MVector mousePos, bool click)
        {
            if (TouchCheck(mousePos))
            {
                //如果碰到鼠标
                if (!WasTouch)
                {
                    //如果之前没有碰到鼠标 => 触碰的瞬间
                    //调用 TouchAction, 将 WasTouch 设为 true
                    TouchAction();
                    WasTouch = true;
                }
                else
                {
                    //如果一直碰到鼠标, 调用 HoverAction
                    HoverAction();
                }

                if (!WasClick && click)
                {
                    //如果之前松开鼠标 (WasClick == false), 且现在按下了鼠标 (click == true) => 单击的瞬间
                    //调用 ClickAction, 将 WasClick 设为 true
                    ClickAction();
                    WasClick = true;
                }
                else if (!click)
                {
                    //如果现在松开鼠标 (click == false)
                    //将 WasClick 设为 false
                    WasClick = false;
                }
                //如果一直长按鼠标 (WasClck == true), 不作反应
            }
            else
            {
                if (WasTouch)
                {
                    DeTouchAction();
                    WasTouch = false;
                }
            }
        }




        /// <summary>鼠标 <b>触碰瞬间</b> 的行为</summary>
        public void TouchAction();

        /// <summary>鼠标 <b>离开瞬间</b> 的行为</summary>
        public void DeTouchAction();

        /// <summary>鼠标 <b>悬浮时</b> 的行为 (重复调用)</summary>
        public void HoverAction();

        /// <summary>鼠标 <b>单击瞬间</b> 的行为</summary>
        public void ClickAction();



        public void Destory()
        {
            foreach(IUIObject uiObj in SubUIObjects)
            {
                uiObj.Destory();
            }
        }
    }
}
