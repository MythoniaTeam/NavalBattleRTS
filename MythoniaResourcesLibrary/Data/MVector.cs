using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;


namespace Mythonia.Resources.Data
{
    public record struct VectorRecord(float X, float Y) : IVector<VectorRecord>
    {
        public Vector2 Vec { get => new Vector2(X, Y); set => throw new NotImplementedException(); }
    }



    public interface IVector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2 Vec { get; set; }
    }

    public interface IVector<Implementation> : IVector 
        where Implementation : IVector<Implementation>, new()
    {

        public static Implementation Constructor(float x, float y)
        {
            Implementation a = new Implementation();
            a.X = x; a.Y = y;
            return a;
        }

        public static Implementation Constructor(Vector2 vec)
        {
            Implementation a = new Implementation();
            a.Vec = vec;
            return a;
        }



        public static Implementation operator +(IVector<Implementation> v1, IVector v2) => Constructor(v1.Vec + v2.Vec);
        public static Implementation operator -(IVector<Implementation> v1, IVector v2) => Constructor(v1.Vec - v2.Vec);
        public static Implementation operator *(IVector<Implementation> v1, IVector v2) => Constructor(v1.Vec * v2.Vec);
        public static Implementation operator /(IVector<Implementation> v1, IVector v2) => Constructor(v1.Vec / v2.Vec);
        public static Implementation operator +(IVector<Implementation> v1, float v2) => Constructor(v1.X + v2, v1.Y + v2);
        public static Implementation operator -(IVector<Implementation> v1, float v2) => Constructor(v1.X - v2, v1.Y - v2);
        public static Implementation operator *(IVector<Implementation> v1, float v2) => Constructor(v1.Vec * v2);
        public static Implementation operator /(IVector<Implementation> v1, float v2) => Constructor(v1.Vec / v2);
        public static Implementation operator -(IVector<Implementation> v1) => Constructor(-v1.Vec);










    }



    public class MVector : IVector<MVector>
    {
        private Vector2 _vector;
        public float X
        {
            get => _vector.X;
            set => _vector.X = value;
        }
        public float Y
        {
            get => _vector.Y;
            set => _vector.Y = value;
        }

        public Vector2 Vec
        {
            get => _vector;
            set => _vector = value;
        }

        public void method(MVector a, IVector b)
        {
            MVector c = a + b;
            method2(a);
        }
        public void method2(IVector<MVector> a)
        {
        }


        public static implicit operator IVector<MVector>(MVector a)
        {
        }



    }
}
