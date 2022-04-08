using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{
    public struct MAngle
    {
        private float _degree = 0;
        public float Degree
        {
            get => _degree;
            set
            {
                if (value is >= 360 or < 0) 
                    _degree = value % 360 
                        + ((value < 0) ? 360 : 0);
                else _degree = value;
            }
        }

        public float Radian
        {
            get => DegToRad(Degree);
            set => Degree = RadToDeg(value);
        }


        /// <summary>正弦值, Sin * 斜边Length = y</summary>
        public float Sin => MathF.Sin(Radian);
        /// <summary>余弦值, Cos * 斜边Length = x</summary>
        public float Cos => MathF.Cos(Radian);
        /// <summary>正切值, Tan = y / x</summary>
        public float Tan => MathF.Tan(Radian);

        /// <summary>获取该方向的 单位向量</summary>
        public MVector ToVector => new MVector(Cos, Sin);


        public MAngle() { }
        public MAngle(float degree) => Degree = degree;
        


        public void method()
        {
            string[] str = { "a", "bc", "def" };
            str.Where(w => w.StartsWith("a")).Min(w => w.Length);
        }







        /// <summary>角度 转 单位向量</summary>
        public static implicit operator MVector(MAngle v) => v.ToVector;
        /// <summary>向量 转 角度</summary>
        public static implicit operator MAngle(MVector v) => v.ToAngle;


        /// <summary>将弧度 转换为角度</summary>
        public static float RadToDeg(float radian) => radian / MathF.PI * 180;
        /// <summary>将角度 转换为弧度</summary>
        public static float DegToRad(float degree) => degree * MathF.PI / 180;

    }
}
