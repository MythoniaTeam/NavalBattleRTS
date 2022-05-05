using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{
    public class MPositionDp<T> : IVector where T : IMVector<T>
    {
        public IMVector<T> Origin;
        public MVector Displacement;



        public Vector2 Vec
        {
            get => Origin.Vec + Displacement.Vec;
            set => Displacement = value - Origin.Vec;
        }
        public float X
        {
            get => Origin.X + Displacement.X;
            set => Displacement.X = value - Origin.X;
        }
        public float Y
        {
            get => Origin.Y + Displacement.Y;
            set => Displacement.Y = value - Origin.Y;
        }


    }
}
