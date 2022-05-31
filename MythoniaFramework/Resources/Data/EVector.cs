


using Mythonia.Resources.Utilities;



namespace Mythonia.Resources.Data
{
    public static class EVector
    {
        public static Vector2 Clone(this Vector2 v) => new Vector2(v.X, v.Y);


        /// <summary>将向量的 X, Y 限制至一个范围内,</summary>
        /// <param name="min">下限</param>
        /// <param name="max">上限</param>
        /// <returns>一个限制后的新对象</returns>
        public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max) => Vector2.Clamp(v, min, max);

        /// <summary>将向量的 X, Y 转为绝对值, 返回到自身</summary>
        public static void Absization(this Vector2 v)
        {
            v.X = MathF.Abs(v.X);
            v.Y = MathF.Abs(v.Y);
        }

        /// <summary>将向量的 X, Y 转为绝对值</summary>
        /// <return>新的 <see cref="MVector"/> 对象</return>
        public static Vector2 Abs(this Vector2 v) => new(MathF.Abs(v.X), MathF.Abs(v.Y));
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        public static void RevertXY(this Vector2 v) => MUtility.Revert(ref v.X, ref v.Y);

        public static void Reflect(this Vector2 v, Vector2 normal)
        {
            Vector2 v2 = Vector2.Reflect(v, normal);
            v.X = v2.X;
            v.Y = v2.Y;
        }
    }
}
