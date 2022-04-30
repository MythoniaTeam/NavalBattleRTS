using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

using Mythonia.Resources.Extensions;


namespace Mythonia.Resources.Data
{

    public struct MVector : IMVector<MVector>, IEquatable<MVector>
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

        public MVector(Vector2 vec) => _vec = vec;
        public MVector(float x, float y) => _vec = new(x, y);
        public MVector(float x) : this(x, x) { }



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






        public static MVector Round(MVector v) => Vector2.Round(v);
        public static MVector Floor(MVector v) => Vector2.Floor(v);
        public static MVector Ceiling(MVector v) => Vector2.Ceiling(v);
        public static MVector Normalize(MVector v) => Vector2.Normalize(v);

        public static MVector Clamp(MVector v, MVector min, MVector max) => Vector2.Clamp(v, min, max);
        public static MVector Min(MVector v1, MVector v2) => Vector2.Min(v1, v2);
        public static MVector Max(MVector v1, MVector v2) => Vector2.Max(v1, v2);

        public static MVector Min(IList<MVector> vs)
        {
            MVector vMin = vs[0];
            foreach (MVector v in vs) vMin = Min(vMin, v);
            return vMin;
        }
        public static MVector Max(IList<MVector> vs)
        {
            MVector vMax = vs[0];
            foreach (MVector v in vs) vMax = Min(vMax, v);
            return vMax;
        }


        public static float Distance(Vector2 v1, Vector2 v2) => Vector2.Distance(v1, v2);
        public static float DistanceSquared(Vector2 v1, Vector2 v2) => Vector2.DistanceSquared(v1, v2);

        public static Vector2 Reflect(Vector2 v, Vector2 normal) => Vector2.Reflect(v, normal);






        public static MVector operator +(MVector v1, MVector v2) => v1.Vec + v2.Vec;
        public static MVector operator -(MVector v1, MVector v2) => v1.Vec - v2.Vec;
        public static MVector operator *(MVector v1, MVector v2) => v1.Vec * v2.Vec;
        public static MVector operator /(MVector v1, MVector v2) => v1.Vec / v2.Vec;
        public static MVector operator %(MVector v1, MVector v2) => new(v1.X % v2.X, v1.Y % v2.Y);


        public static MVector operator +(MVector v1, float v2) => v1.Vec + new Vector2(v2);
        public static MVector operator -(MVector v1, float v2) => v1.Vec - new Vector2(v2);
        public static MVector operator *(MVector v1, float v2) => v1.Vec * v2;
        public static MVector operator /(MVector v1, float v2) => v1.Vec / v2;

        public static MVector operator +(float v2, MVector v1) => v1.Vec + new Vector2(v2);
        public static MVector operator -(float v2, MVector v1) => v1.Vec - new Vector2(v2);
        public static MVector operator *(float v2, MVector v1) => v1.Vec * v2;
        public static MVector operator /(float v2, MVector v1) => v1.Vec / v2;


        public static MVector operator -(MVector v1) => -v1.Vec;


        public static bool operator <(MVector v1, MVector v2) => v1._SnS(v2);
        public static bool operator >(MVector v1, MVector v2) => v1._LnL(v2);

        public static bool operator ==(MVector v1, float v2) => v1.X == v2 && v1.Y == v2;
        public static bool operator !=(MVector v1, float v2) => v1.X != v2 && v1.Y != v2;
        public static bool operator <=(MVector v1, float v2) => v1.X <= v2 && v1.Y <= v2;
        public static bool operator >=(MVector v1, float v2) => v1.X >= v2 && v1.Y >= v2;
        public static bool operator <(MVector v1, float v2) => v1.X < v2 && v1.Y < v2;
        public static bool operator >(MVector v1, float v2) => v1.X > v2 && v1.Y > v2;
        public static bool operator ==(float v1, MVector v2) => v1 == v2.X && v1 == v2.Y;
        public static bool operator !=(float v1, MVector v2) => v1 != v2.X && v1 != v2.Y;
        public static bool operator <=(float v1, MVector v2) => v1 <= v2.X && v1 <= v2.Y;
        public static bool operator >=(float v1, MVector v2) => v1 >= v2.X && v1 >= v2.Y;
        public static bool operator <(float v1, MVector v2) => v1 < v2.X && v1 < v2.Y;
        public static bool operator >(float v1, MVector v2) => v1 > v2.X && v1 > v2.Y;




        /// <summary>X比v大, 且Y比v大<br/>v位于BL位置内</summary>
        public bool _LnL(MVector v) => X > v.X && Y > v.Y;
        /// <summary>X比v小, 且Y比v大<br/>v位于BR位置内</summary>
        public bool _SnL(MVector v) => X < v.X && Y > v.Y;
        /// <summary>X比v大, 且Y比v小<br/>v位于TL位置内</summary>
        public bool _LnS(MVector v) => X > v.X && Y < v.Y;
        /// <summary>X比v小, 且Y比v小<br/>v位于TR位置内</summary>
        public bool _SnS(MVector v) => X < v.X && Y < v.Y;


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于BL位置内, 或边缘上</summary>
        public bool _LEnLE(MVector v) => X >= v.X && Y >= v.Y;
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于BR位置内, 或边缘上</summary>
        public bool _SEnLE(MVector v) => X <= v.X && Y >= v.Y;
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于TL位置内, 或边缘上</summary>
        public bool _LEnSE(MVector v) => X >= v.X && Y <= v.Y;
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于TR位置内, 或边缘上</summary>
        public bool _SEnSE(MVector v) => X <= v.X && Y <= v.Y;


        /// <summary>X比v大, 或Y比v大<br/>v位于TR位置外</summary>
        public bool _LoL(MVector v) => X > v.X || Y > v.Y;
        /// <summary>X比v小, 或Y比v大<br/>v位于TL位置外</summary>
        public bool _SoL(MVector v) => X < v.X || Y > v.Y;
        /// <summary>X比v大, 或Y比v小<br/>v位于BR位置外</summary>
        public bool _LoS(MVector v) => X > v.X || Y < v.Y;
        /// <summary>X比v小, 或Y比v小<br/>v位于BL位置外</summary>
        public bool _SoS(MVector v) => X < v.X || Y < v.Y;


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于TR位置外, 或边缘上</summary>
        public bool _LEoLE(MVector v) => X >= v.X || Y >= v.Y;
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于TL位置外, 或边缘上</summary>
        public bool _SEoLE(MVector v) => X <= v.X || Y >= v.Y;
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于BR位置外, 或边缘上</summary>
        public bool _LEoSE(MVector v) => X >= v.X || Y <= v.Y;
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于BL位置外, 或边缘上</summary>
        public bool _SEoSE(MVector v) => X <= v.X || Y <= v.Y;

        public bool _EoE(MVector v) => X == v.X || Y == v.Y;
        public bool _EoL(MVector v) => X == v.X || Y > v.Y;
        public bool _EoLE(MVector v) => X == v.X || Y >= v.Y;
        public bool _EoS(MVector v) => X == v.X || Y < v.Y;
        public bool _EoSE(MVector v) => X == v.X || Y <= v.Y;
        public bool _LoE(MVector v) => X > v.X || Y == v.Y;
        public bool _LEoE(MVector v) => X >= v.X || Y == v.Y;
        public bool _SoE(MVector v) => X < v.X || Y == v.Y;
        public bool _SEoE(MVector v) => X <= v.X || Y == v.Y;

        public bool _EnE(MVector v) => X == v.X && Y == v.Y;
        public bool _EnL(MVector v) => X == v.X && Y > v.Y;
        public bool _EnLE(MVector v) => X == v.X && Y >= v.Y;
        public bool _EnS(MVector v) => X == v.X && Y < v.Y;
        public bool _EnSE(MVector v) => X == v.X && Y <= v.Y;
        public bool _LnE(MVector v) => X > v.X && Y == v.Y;
        public bool _LEnE(MVector v) => X >= v.X && Y == v.Y;
        public bool _SnE(MVector v) => X < v.X && Y == v.Y;
        public bool _SEnE(MVector v) => X <= v.X && Y == v.Y;



        public MVector Clone() => new MVector(Vec);

        public bool Equals(MVector other) => _EnE(other);

        public override string ToString() => $"(X: {X}, Y: {Y})";




        /// <summary>MVector 转 XNA.Vector2</summary>
        public static implicit operator Vector2(MVector v) => v.Vec;
        public static implicit operator Point(MVector v) => v.Vec.ToPoint();

        /// <summary>XNA.Vector2 转 MVector</summary>
        public static implicit operator MVector(Vector2 v) => new MVector(v);



        /// <summary>MVector(值类型) 转 MVectorR(引用类型)</summary>
        public static implicit operator MPosition(MVector v) => new MPosition(v.Vec);
        /// <summary>MVectorR(引用类型) 转 MVector(值类型)</summary>
        public static implicit operator MVector(MPosition v) => new MVector(v.Vec);

        ///// <summary>Record 转 MVector</summary>
        //public static implicit operator MVector(VectorRecord v) => new MVector(v.Vec);






        public static implicit operator (float, float)(MVector v) => (v.X, v.Y);
        public static implicit operator MVector((float, float) v) => new(v.Item1, v.Item2);


        public static explicit operator MVector(string v)
        {
            string[] vSplited = v.Split(',');
            if (vSplited.Length != 2) throw new Exception($"The Given String has {vSplited.Length} elements after Splited by ',' but there Should have 2");
            float x = float.Parse(vSplited[0]);
            float y = float.Parse(vSplited[1]);
            return new(x, y);
        }

    }


    


    
}
