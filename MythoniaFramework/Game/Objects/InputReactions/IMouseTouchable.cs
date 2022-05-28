


namespace Mythonia.Game.Objects.InputReactions
{
    public interface IMouseTouchable
    {
        protected IMouseTouchDetector Detector { get; }

        public bool Touched { get; set; }


        public void CheckTouch(MVector mousePosition)
        {
            if(Detector.IsTouch(mousePosition))
            {
                //碰到了鼠标, 且上一次循环没有碰到
                if (!Touched)
                {
                    TouchAction();

                    Touched = true;
                }
            }
            else
            {
                //没有碰到鼠标, 且上一次循环碰到了
                if (Touched)
                {
                    UnTouchAction();
                    Touched = false;
                }
            }
        }
        
        public void TouchAction();
        public void UnTouchAction();


    }
}
