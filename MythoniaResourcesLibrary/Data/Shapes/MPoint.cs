using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data.Shapes
{
    public class MPoint : IPoint
    {
        private IVector _position;
        public IVector Position
        {
            get => _position;
            set => _position = value;
        }
        public float X
        {
            get => _position.X;
            set => _position.X = value;
        }
        public float Y
        {
            get => _position.Y;
            set => _position.Y = value;
        }


        public MPoint(MVector position) => Position = position;
        public MPoint(float x, float y) : this(new(x, y)) { }


        public static implicit operator MVector(MPoint pt) => pt.Position.Vec;
        public static implicit operator MPoint(MVector pt) => new(pt);
    }
}
