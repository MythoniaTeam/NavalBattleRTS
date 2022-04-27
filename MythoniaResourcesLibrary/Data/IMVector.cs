using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{
    public interface IMVector<T> : IVector where T : IMVector<T>
    {
        public float Length { get; }
        public float LengthSquared { get; }

        public float Direction { get; }
        public MAngle ToAngle { get; }



        public void Round();
        /// <summary>将向量向下取整</summary>
        public void Floor();
        /// <summary>将向量向上取整</summary>
        public void Ceiling();
        /// <summary>化为单位向量</summary>
        public void Normalize();
        public void Clamp(Vector2 min, Vector2 max);

        public void RevertXY();

        public void ChangeSign(float xSign, float ySign);
        public void ChangeSign(Vector2 Sign);
        public void Abs();

        public void Reflect(Vector2 normal);




        public bool _LnL(T v);
        /// <summary>X比v小, 且Y比v大<br/>v位于BR位置内</summary>
        public bool _SnL(T v);
        /// <summary>X比v大, 且Y比v小<br/>v位于TL位置内</summary>
        public bool _LnS(T v);
        /// <summary>X比v小, 且Y比v小<br/>v位于TR位置内</summary>
        public bool _SnS(T v);


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于BL位置内, 或边缘上</summary>
        public bool _LEnLE(T v);
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于BR位置内, 或边缘上</summary>
        public bool _SEnLE(T v);
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于TL位置内, 或边缘上</summary>
        public bool _LEnSE(T v);
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于TR位置内, 或边缘上</summary>
        public bool _SEnSE(T v);


        /// <summary>X比v大, 或Y比v大<br/>v位于TR位置外</summary>
        public bool _LoL(T v);
        /// <summary>X比v小, 或Y比v大<br/>v位于TL位置外</summary>
        public bool _SoL(T v);
        /// <summary>X比v大, 或Y比v小<br/>v位于BR位置外</summary>
        public bool _LoS(T v);
        /// <summary>X比v小, 或Y比v小<br/>v位于BL位置外</summary>
        public bool _SoS(T v);


        /// <summary>X比v大 或相等, 且Y比v大 或相等<br/>v位于TR位置外, 或边缘上</summary>
        public bool _LEoLE(T v);
        /// <summary>X比v小 或相等, 且Y比v大 或相等<br/>v位于TL位置外, 或边缘上</summary>
        public bool _SEoLE(T v);
        /// <summary>X比v大 或相等, 且Y比v小 或相等<br/>v位于BR位置外, 或边缘上</summary>
        public bool _LEoSE(T v);
        /// <summary>X比v小 或相等, 且Y比v小 或相等<br/>v位于BL位置外, 或边缘上</summary>
        public bool _SEoSE(T v);

        public bool _EoE(T v);
        public bool _EoL(T v);
        public bool _EoLE(T v);
        public bool _EoS(T v);
        public bool _EoSE(T v);
        public bool _LoE(T v);
        public bool _LEoE(T v);
        public bool _SoE(T v);
        public bool _SEoE(T v);

        public bool _EnE(T v);
        public bool _EnL(T v);
        public bool _EnLE(T v);
        public bool _EnS(T v);
        public bool _EnSE(T v);
        public bool _LnE(T v);
        public bool _LEnE(T v);
        public bool _SnE(T v);
        public bool _SEnE(T v);



        public T Clone();

        public bool Equals(T other);
    }
}
