using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game
{
    public class Camera
    {
        MGame MGame;

        public MPosition Position = new(0);

        public MVector Scale = new(1);

        public Camera(MGame game, MPosition pos)
        {
            MGame = game;
            Position = pos;
        }

        /// <summary>
        /// 给定游戏场景中的坐标 (y向上增加, 坐标相对于Camera.Pos)
        /// </summary>
        /// <param name="WorldPos"></param>
        /// <returns>绘制坐标 (y向下增加, 坐标相对于屏幕左上角)</returns>
        public MVector ToScreenPos(MVector gamePos)
            => ToTopLeftPos((gamePos - (MVector)Position) * Scale);

        public MVector ToTopLeftPos(MVector centerPos)
            => centerPos.ChangeSignY() + MGame.GraphicsDevice.Viewport.Size() / 2;
        
    }
}
