using System;
using System.Collections.Generic;
using System.Text;

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

        public MVectorR(Vector2 vec) => _vec = vec;
        public MVectorR(float x, float y) => _vec = new Vector2(x, y);
    }
}
