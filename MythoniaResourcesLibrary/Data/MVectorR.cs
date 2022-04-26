using System;
using System.Collections.Generic;
using System.Text;

using Mythonia.Resources.Extensions;

namespace Mythonia.Resources.Data
{
    public class MVectorR : IVector
    {
        private Vector2 _vec;
        public float X
        {
            get => _vec.X;
            set => _vec.X = value;
        }
        public float Y
        {
            get => _vec.Y;
            set => _vec.Y = value;
        }


        public Vector2 Vec => _vec;

        public float Length => _vec.Length();
        public float LengthSquared => _vec.LengthSquared();

        public float Direction => MathF.Atan2(Y, X);
        public MAngle ToAngle => new(Direction);

        public MVectorR(Vector2 vec) => _vec = vec;
        public MVectorR(float x, float y) => _vec = new(x, y);
        public MVectorR(float x) : this(x, x) { }



        public void Round() => _vec.Round();
        /// <summary>将向量向下取整</summary>
        public void Floor() => _vec.Floor();
        /// <summary>将向量向上取整</summary>
        public void Ceiling() => _vec.Ceiling();
        /// <summary>化为单位向量</summary>
        public void Normalize() => _vec.Normalize();
        public void Clamp(Vector2 min, Vector2 max) => _vec.Clamp(min, max);

        public void RevertXY() => _vec.RevertXY();

        public void ChangeSign(float xSign, float ySign) => ChangeSign(new(xSign, ySign));
        public void ChangeSign(Vector2 Sign) => _vec *= Sign;
        public void Abs() => _vec.Absolutization();

        public void Reflect(Vector2 normal) => _vec.Reflect(normal);






        public static MVectorR Round(MVectorR v) => Vector2.Round(v);
        public static MVectorR Floor(MVectorR v) => Vector2.Floor(v);
        public static MVectorR Ceiling(MVectorR v) => Vector2.Ceiling(v);
        public static MVectorR Normalize(MVectorR v) => Vector2.Normalize(v);

        public static MVectorR Clamp(MVectorR v, MVectorR min, MVectorR max) => Vector2.Clamp(v, min, max);
        public static MVectorR Min(MVectorR v1, MVectorR v2) => Vector2.Min(v1, v2);
        public static MVectorR Max(MVectorR v1, MVectorR v2) => Vector2.Max(v1, v2);

        public static MVectorR Min(IList<MVectorR> vs)
        {
            MVectorR vMin = vs[0];
            foreach (MVectorR v in vs) vMin = Min(vMin, v);
            return vMin;
        }
        public static MVectorR Max(IList<MVectorR> vs)
        {
            MVectorR vMax = vs[0];
            foreach (MVectorR v in vs) vMax = Min(vMax, v);
            return vMax;
        }


        public static float Distance(Vector2 v1, Vector2 v2) => Vector2.Distance(v1, v2);
        public static float DistanceSquared(Vector2 v1, Vector2 v2) => Vector2.DistanceSquared(v1, v2);

        public static Vector2 Reflect(Vector2 v, Vector2 normal) => Vector2.Reflect(v, normal);






        public static MVectorR operator +(MVectorR v1, MVectorR v2) => v1.Vec + v2.Vec;
        public static MVectorR operator -(MVectorR v1, MVectorR v2) => v1.Vec - v2.Vec;
        public static MVectorR operator *(MVectorR v1, MVectorR v2) => v1.Vec * v2.Vec;
        public static MVectorR operator /(MVectorR v1, MVectorR v2) => v1.Vec / v2.Vec;
        public static MVectorR operator %(MVectorR v1, MVectorR v2) => new(v1.X % v2.X, v1.Y % v2.Y);


        public static MVectorR operator +(MVectorR v1, float v2) => v1.Vec + new Vector2(v2);
        public static MVectorR operator -(MVectorR v1, float v2) => v1.Vec - new Vector2(v2);
        public static MVectorR operator *(MVectorR v1, float v2) => v1.Vec * v2;
        public static MVectorR operator /(MVectorR v1, float v2) => v1.Vec / v2;


        public static MVectorR operator -(MVectorR v1) => -v1.Vec;


        public static bool operator <(MVectorR v1, MVectorR v2) => v1._SnS(v2);
        public static bool operator >(MVectorR v1, MVectorR v2) => v1._LnL(v2);
        public static bool operator ==(MVectorR v1, float v2) => v1.X == v2 && v1.Y == v2;
        public static bool operator !=(MVectorR v1, float v2) => v1.X != v2 && v1.Y != v2;
        public static bool operator <=(MVectorR v1, float v2) => v1.X <= v2 && v1.Y <= v2;
        public static bool operator >=(MVectorR v1, float v2) => v1.X >= v2 && v1.Y >= v2;
        public static bool operator <(MVectorR v1, float v2) => v1.X < v2 && v1.Y < v2;
        public static bool operator >(MVectorR v1, float v2) => v1.X > v2 && v1.Y > v2;
        public static bool operator ==(float v1, MVectorR v2) => v1 == v2.X && v1 == v2.Y;
        public static bool operator !=(float v1, MVectorR v2) => v1 != v2.X && v1 != v2.Y;
        public static bool operator <=(float v1, MVectorR v2) => v1 <= v2.X && v1 <= v2.Y;
        public static bool operator >=(float v1, MVectorR v2) => v1 >= v2.X && v1 >= v2.Y;
        public static bool operator <(float v1, MVectorR v2) => v1 < v2.X && v1 < v2.Y;
        public static bool operator >(float v1, MVectorR v2) => v1 > v2.X && v1 > v2.Y;




        /// <summary>X比v大, 且Y比v大<br/>v位于BL位置内</summary>
        public bool _LnL(MVectorR v) => X > v.X && Y > v.Y;
        /// <summary>X比v小, 且Y比v大<br/>v位于BR位置内</summary>
        public bool _SnL(MVectorR v) => X < v.X && Y > v.Y;
        /// <summary>X比v大, 且Y比v小<br/>v位于TL位置内</summary>
        public bool _LnS(MVectorR v) => X > v.X && Y < v.Y;
        /// <summary>X比v小, 且Y比v小<br/>v位于TR位置内</summary>
        public bool _SnS(MVectorR v) => X < v.X && Y < v.Y;


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于BL位置内, 或边缘上</summary>
        public bool _LEnLE(MVectorR v) => X >= v.X && Y >= v.Y;
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于BR位置内, 或边缘上</summary>
        public bool _SEnLE(MVectorR v) => X <= v.X && Y >= v.Y;
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于TL位置内, 或边缘上</summary>
        public bool _LEnSE(MVectorR v) => X >= v.X && Y <= v.Y;
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于TR位置内, 或边缘上</summary>
        public bool _SEnSE(MVectorR v) => X <= v.X && Y <= v.Y;


        /// <summary>X比v大, 或Y比v大<br/>v位于TR位置外</summary>
        public bool _LoL(MVectorR v) => X > v.X || Y > v.Y;
        /// <summary>X比v小, 或Y比v大<br/>v位于TL位置外</summary>
        public bool _SoL(MVectorR v) => X < v.X || Y > v.Y;
        /// <summary>X比v大, 或Y比v小<br/>v位于BR位置外</summary>
        public bool _LoS(MVectorR v) => X > v.X || Y < v.Y;
        /// <summary>X比v小, 或Y比v小<br/>v位于BL位置外</summary>
        public bool _SoS(MVectorR v) => X < v.X || Y < v.Y;


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于TR位置外, 或边缘上</summary>
        public bool _LEoLE(MVectorR v) => X >= v.X || Y >= v.Y;
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于TL位置外, 或边缘上</summary>
        public bool _SEoLE(MVectorR v) => X <= v.X || Y >= v.Y;
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于BR位置外, 或边缘上</summary>
        public bool _LEoSE(MVectorR v) => X >= v.X || Y <= v.Y;
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于BL位置外, 或边缘上</summary>
        public bool _SEoSE(MVectorR v) => X <= v.X || Y <= v.Y;

        public bool _EoE(MVectorR v) => X == v.X || Y == v.Y;
        public bool _EoL(MVectorR v) => X == v.X || Y > v.Y;
        public bool _EoLE(MVectorR v) => X == v.X || Y >= v.Y;
        public bool _EoS(MVectorR v) => X == v.X || Y < v.Y;
        public bool _EoSE(MVectorR v) => X == v.X || Y <= v.Y;
        public bool _LoE(MVectorR v) => X > v.X || Y == v.Y;
        public bool _LEoE(MVectorR v) => X >= v.X || Y == v.Y;
        public bool _SoE(MVectorR v) => X < v.X || Y == v.Y;
        public bool _SEoE(MVectorR v) => X <= v.X || Y == v.Y;

        public bool _EnE(MVectorR v) => X == v.X && Y == v.Y;
        public bool _EnL(MVectorR v) => X == v.X && Y > v.Y;
        public bool _EnLE(MVectorR v) => X == v.X && Y >= v.Y;
        public bool _EnS(MVectorR v) => X == v.X && Y < v.Y;
        public bool _EnSE(MVectorR v) => X == v.X && Y <= v.Y;
        public bool _LnE(MVectorR v) => X > v.X && Y == v.Y;
        public bool _LEnE(MVectorR v) => X >= v.X && Y == v.Y;
        public bool _SnE(MVectorR v) => X < v.X && Y == v.Y;
        public bool _SEnE(MVectorR v) => X <= v.X && Y == v.Y;



        public MVectorR Clone() => new MVectorR(Vec);

        public bool Equals(MVectorR other) => _EnE(other);

        public override string ToString() => $"(X: {X}, Y: {Y})";




        /// <summary>MVectorR 转 XNA.Vector2</summary>
        public static implicit operator Vector2(MVectorR v) => v.Vec;
        public static implicit operator Point(MVectorR v) => v.Vec.ToPoint();

        /// <summary>XNA.Vector2 转 MVectorR</summary>
        public static implicit operator MVectorR(Vector2 v) => new MVectorR(v);
    }
