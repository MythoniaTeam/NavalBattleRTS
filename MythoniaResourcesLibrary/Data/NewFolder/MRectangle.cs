using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Data
{
    


    public class MRectangle : IRectangle
    {
        private MVector _size;
        private MVector _position;
        
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
        public MVector Position
        {
            get => _position;
            set => _position = value;
        }


        public float Width
        {
            get => _size.X;
            set => _size.X = value;
        }
        public float Height
        {
            get => _size.Y;
            set => _size.Y = value;
        }
        public MVector Size
        {
            get => _size;
            set => _size = value;
        }

        public IRectangle Interfacenize() => this;

    }
}
