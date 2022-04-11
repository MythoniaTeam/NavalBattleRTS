using System;
using System.Collections.Generic;
using System.Text;

using Mythonia.Resources.Utilities;



namespace Mythonia.Resources.Extensions
{
    public static class EVector
    {
        public static Vector2 Clone(this Vector2 v) => new Vector2(v.X, v.Y);

        public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max) => Vector2.Clamp(v, min, max);

        public static void Absolutization(this Vector2 v)
        {
            v.X = MathF.Abs(v.X);
            v.Y = MathF.Abs(v.Y);
        }

        public static void RevertXY(this Vector2 v) => MUtility.Revert(ref v.X, ref v.Y);

        public static void Reflect(this Vector2 v, Vector2 normal)
        {
            Vector2 v2 = Vector2.Reflect(v, normal);
            v.X = v2.X;
            v.Y = v2.Y;
        }
    }
}
