using System;
using System.Collections.Generic;
using System.Text;

using Mythonia.Resources.Extensions;

namespace Mythonia.Resources.Data
{
    /// <summary>
    /// 其实就是MVector的引用类型版
    /// </summary>
    public class MPosition : IMVector<MPosition>
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

        public MPosition(Vector2 vec) => _vec = vec;
        public MPosition(float x, float y) => _vec = new(x, y);
        public MPosition(float x) : this(x, x) { }



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






        public static MPosition Round(MPosition v) => Vector2.Round(v);
        public static MPosition Floor(MPosition v) => Vector2.Floor(v);
        public static MPosition Ceiling(MPosition v) => Vector2.Ceiling(v);
        public static MPosition Normalize(MPosition v) => Vector2.Normalize(v);

        public static MPosition Clamp(MPosition v, MPosition min, MPosition max) => Vector2.Clamp(v, min, max);
        public static MPosition Min(MPosition v1, MPosition v2) => Vector2.Min(v1, v2);
        public static MPosition Max(MPosition v1, MPosition v2) => Vector2.Max(v1, v2);

        public static MPosition Min(IList<MPosition> vs)
        {
            MPosition vMin = vs[0];
            foreach (MPosition v in vs) vMin = Min(vMin, v);
            return vMin;
        }
        public static MPosition Max(IList<MPosition> vs)
        {
            MPosition vMax = vs[0];
            foreach (MPosition v in vs) vMax = Min(vMax, v);
            return vMax;
        }






        public static MPosition operator +(MPosition v1, MPosition v2) => v1.Vec + v2.Vec;
        public static MPosition operator -(MPosition v1, MPosition v2) => v1.Vec - v2.Vec;
        public static MPosition operator *(MPosition v1, MPosition v2) => v1.Vec * v2.Vec;
        public static MPosition operator /(MPosition v1, MPosition v2) => v1.Vec / v2.Vec;
        public static MPosition operator %(MPosition v1, MPosition v2) => new(v1.X % v2.X, v1.Y % v2.Y);


        public static MPosition operator +(MPosition v1, float v2) => v1.Vec + new Vector2(v2);
        public static MPosition operator -(MPosition v1, float v2) => v1.Vec - new Vector2(v2);
        public static MPosition operator *(MPosition v1, float v2) => v1.Vec * v2;
        public static MPosition operator /(MPosition v1, float v2) => v1.Vec / v2;


        public static MPosition operator -(MPosition v1) => -v1.Vec;


        public static bool operator <(MPosition v1, MPosition v2) => v1._SnS(v2);
        public static bool operator >(MPosition v1, MPosition v2) => v1._LnL(v2);
        public static bool operator ==(MPosition v1, float v2) => v1.X == v2 && v1.Y == v2;
        public static bool operator !=(MPosition v1, float v2) => v1.X != v2 && v1.Y != v2;
        public static bool operator <=(MPosition v1, float v2) => v1.X <= v2 && v1.Y <= v2;
        public static bool operator >=(MPosition v1, float v2) => v1.X >= v2 && v1.Y >= v2;
        public static bool operator <(MPosition v1, float v2) => v1.X < v2 && v1.Y < v2;
        public static bool operator >(MPosition v1, float v2) => v1.X > v2 && v1.Y > v2;
        public static bool operator ==(float v1, MPosition v2) => v1 == v2.X && v1 == v2.Y;
        public static bool operator !=(float v1, MPosition v2) => v1 != v2.X && v1 != v2.Y;
        public static bool operator <=(float v1, MPosition v2) => v1 <= v2.X && v1 <= v2.Y;
        public static bool operator >=(float v1, MPosition v2) => v1 >= v2.X && v1 >= v2.Y;
        public static bool operator <(float v1, MPosition v2) => v1 < v2.X && v1 < v2.Y;
        public static bool operator >(float v1, MPosition v2) => v1 > v2.X && v1 > v2.Y;




        /// <summary>X比v大, 且Y比v大<br/>v位于BL位置内</summary>
        public bool _LnL(MPosition v) => X > v.X && Y > v.Y;
        /// <summary>X比v小, 且Y比v大<br/>v位于BR位置内</summary>
        public bool _SnL(MPosition v) => X < v.X && Y > v.Y;
        /// <summary>X比v大, 且Y比v小<br/>v位于TL位置内</summary>
        public bool _LnS(MPosition v) => X > v.X && Y < v.Y;
        /// <summary>X比v小, 且Y比v小<br/>v位于TR位置内</summary>
        public bool _SnS(MPosition v) => X < v.X && Y < v.Y;


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于BL位置内, 或边缘上</summary>
        public bool _LEnLE(MPosition v) => X >= v.X && Y >= v.Y;
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于BR位置内, 或边缘上</summary>
        public bool _SEnLE(MPosition v) => X <= v.X && Y >= v.Y;
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于TL位置内, 或边缘上</summary>
        public bool _LEnSE(MPosition v) => X >= v.X && Y <= v.Y;
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于TR位置内, 或边缘上</summary>
        public bool _SEnSE(MPosition v) => X <= v.X && Y <= v.Y;


        /// <summary>X比v大, 或Y比v大<br/>v位于TR位置外</summary>
        public bool _LoL(MPosition v) => X > v.X || Y > v.Y;
        /// <summary>X比v小, 或Y比v大<br/>v位于TL位置外</summary>
        public bool _SoL(MPosition v) => X < v.X || Y > v.Y;
        /// <summary>X比v大, 或Y比v小<br/>v位于BR位置外</summary>
        public bool _LoS(MPosition v) => X > v.X || Y < v.Y;
        /// <summary>X比v小, 或Y比v小<br/>v位于BL位置外</summary>
        public bool _SoS(MPosition v) => X < v.X || Y < v.Y;


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于TR位置外, 或边缘上</summary>
        public bool _LEoLE(MPosition v) => X >= v.X || Y >= v.Y;
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于TL位置外, 或边缘上</summary>
        public bool _SEoLE(MPosition v) => X <= v.X || Y >= v.Y;
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于BR位置外, 或边缘上</summary>
        public bool _LEoSE(MPosition v) => X >= v.X || Y <= v.Y;
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于BL位置外, 或边缘上</summary>
        public bool _SEoSE(MPosition v) => X <= v.X || Y <= v.Y;

        public bool _EoE(MPosition v) => X == v.X || Y == v.Y;
        public bool _EoL(MPosition v) => X == v.X || Y > v.Y;
        public bool _EoLE(MPosition v) => X == v.X || Y >= v.Y;
        public bool _EoS(MPosition v) => X == v.X || Y < v.Y;
        public bool _EoSE(MPosition v) => X == v.X || Y <= v.Y;
        public bool _LoE(MPosition v) => X > v.X || Y == v.Y;
        public bool _LEoE(MPosition v) => X >= v.X || Y == v.Y;
        public bool _SoE(MPosition v) => X < v.X || Y == v.Y;
        public bool _SEoE(MPosition v) => X <= v.X || Y == v.Y;

        public bool _EnE(MPosition v) => X == v.X && Y == v.Y;
        public bool _EnL(MPosition v) => X == v.X && Y > v.Y;
        public bool _EnLE(MPosition v) => X == v.X && Y >= v.Y;
        public bool _EnS(MPosition v) => X == v.X && Y < v.Y;
        public bool _EnSE(MPosition v) => X == v.X && Y <= v.Y;
        public bool _LnE(MPosition v) => X > v.X && Y == v.Y;
        public bool _LEnE(MPosition v) => X >= v.X && Y == v.Y;
        public bool _SnE(MPosition v) => X < v.X && Y == v.Y;
        public bool _SEnE(MPosition v) => X <= v.X && Y == v.Y;



        public MPosition Clone() => new MPosition(Vec);

        public bool Equals(MPosition other) => _EnE(other);

        public override string ToString() => $"(X: {X}, Y: {Y})";




        /// <summary>MVectorR 转 XNA.Vector2</summary>
        public static implicit operator Vector2(MPosition v) => v.Vec;
        public static implicit operator Point(MPosition v) => v.Vec.ToPoint();

        /// <summary>XNA.Vector2 转 MVectorR</summary>
        public static implicit operator MPosition(Vector2 v) => new MPosition(v);
    }
