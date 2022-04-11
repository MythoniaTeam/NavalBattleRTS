using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{
    public class MRay : IPoint
    {
        public MAngle Direction { get; private set; }
        public float Length;

        private MVector _position;
        public MVector Position
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

        public MRay(MVector position, float length, MAngle? direction = null)
        {
            Position = position;
            Length = length;
            Direction = direction ?? new(0);
        }
    }
}
