using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{
    public class MPosition : IVector
    {
        private MVector _vec;
        public MVector Vec { get => _vec; set => _vec = value; }


        //---------- Implement - IVector ----------
        Vector2 IVector.Vec { get => _vec; }
        public float X { get => _vec.X; set => _vec.X = value; }
        public float Y { get => _vec.Y; set => _vec.Y = value; }

        //----------------------------------------



        public MPosition(float x) => Vec = new(x);
        public MPosition(float x, float y) => Vec = new(x, y);
        public MPosition(MVector v) => Vec = new(v.X, v.Y);



        public static implicit operator MPosition(MVector v) => new(v);
        public static implicit operator MVector(MPosition v) => v.Vec;


    }
}
