using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game
{
    public class Utility
    {
        static Texture2D[] Pixel = new Texture2D[3];

        public static MGame Game;
        public static void Initialize(MGame game)
        {
            Game = game;
            Pixel[0] = Game.Content.Load<Texture2D>(@"Images\PX");
            Pixel[1] = Game.Content.Load<Texture2D>(@"Images\PX_RED");
            Pixel[2] = Game.Content.Load<Texture2D>(@"Images\PX_BLACK");

        }

        /// <summary>
        /// 按照屏幕坐标绘制直线
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public static void DrawLine(int pixelTextureType, SpriteBatch spriteBatch, Vector2 begin, Vector2 end, Color color, int width = 1)
        {
            Rectangle r = new Rectangle((int)begin.X, (int)begin.Y, (int)(end - begin).Length() + width, width);
            Vector2 v = Vector2.Normalize(begin - end);
            float angle = (float)Math.Acos(Vector2.Dot(v, -Vector2.UnitX));
            if (begin.Y > end.Y) angle = MathHelper.TwoPi - angle;
            spriteBatch.Draw(Pixel[pixelTextureType], r, null, color, angle, Vector2.Zero, SpriteEffects.None, 0);

            Debug.WriteLine($"DrawLine fr: {begin} to: {end}");
        }

        public static void DrawLineY(int pixelTextureType, float x, Color color, int width = 1)
            => DrawLineY(pixelTextureType, -99999, 99999, x, color, width);
        public static void DrawLineX(int pixelTextureType, float y, Color color, int width = 1)
            => DrawLineX(pixelTextureType, -99999, 99999, y, color, width);


        public static void DrawLineY(int pixelTextureType, float begin, float end, float x, Color color, int width = 1, bool screenPos = false)
        {
            float max = MathF.Max(begin, end);
            float min = MathF.Min(begin, end);

            MVector pMax = screenPos ? Game.CurrentCamera.ToTopLeftPos(new(x, min)) : Game.CurrentCamera.ToScreenPos(new(x, min));
            MVector pMin = screenPos ? Game.CurrentCamera.ToTopLeftPos(new(x, max)) : Game.CurrentCamera.ToScreenPos(new(x, max));

            Game.SpriteBatch.Draw(Pixel[pixelTextureType], pMin, null, color, 0, Vector2.Zero, new Vector2(width, pMax.Y - pMin.Y), SpriteEffects.None, 0);
        }

        public static void DrawLineX(int pixelTextureType, float begin, float end, float y, Color color, int width = 1, bool screenPos = false)
        {
            float max = MathF.Max(begin, end);
            float min = MathF.Min(begin, end);

            MVector pMax = screenPos ? Game.CurrentCamera.ToTopLeftPos(new(max, y)) : Game.CurrentCamera.ToScreenPos(new(max, y));
            MVector pMin = screenPos ? Game.CurrentCamera.ToTopLeftPos(new(min, y)) : Game.CurrentCamera.ToScreenPos(new(min, y));

            Game.SpriteBatch.Draw(Pixel[pixelTextureType], pMin, null, color, 0, Vector2.Zero, new Vector2(pMax.X - pMin.X, width), SpriteEffects.None, 0);
        }



        /// <summary>
        /// 按照场景坐标绘制直线
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="color"></param>
        /// <param name="ScreenPos"></param>
        /// <param name="width"></param>
        public static void DrawLine(int pixelTextureType, SpriteBatch spriteBatch, Vector2 a, Vector2 b, Color color, bool ScreenPos, int width = 1)
        {
            a = Game.CurrentCamera.ToScreenPos(a);
            b = Game.CurrentCamera.ToScreenPos(b);

            DrawLine(pixelTextureType, spriteBatch, a, b, color, width);
        }

        /// <summary>
        /// 直接使用int传值
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="color"></param>
        /// <param name="width"></param>
        public static void DrawLine(int pixelTextureType, SpriteBatch spriteBatch,
            int ax, int ay, int bx, int by, Color color, int width = 1)
            => DrawLine(pixelTextureType, spriteBatch, new Vector2(ax, ay), new Vector2(bx, by), color, true, width);


        /*public static void DrawLine(int pixelTextureType, SpriteBatch spriteBatch,
            MLine line, Color color, int width = 1)
            => DrawLine(pixelTextureType, spriteBatch, line.A, line.B, color, true, width);*/
    }
}
