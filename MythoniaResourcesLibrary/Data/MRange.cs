
using System.Collections;

namespace Mythonia.Resources.Data
{
    public class MRange
    {
        public float Min;
        public float Max;

        public MRange(float value)
        {
            Min = Max = value;
        }

        public MRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        //public MRange(IEnumerable list, Func<object, float> func) 
        //{
        //    foreach(object obj in list)
        //    {
        //        func(obj);
        //    }
        //}

        public void AddValue(float value)
        {
            if (value < Min) Min = value;
            else if (value > Max) Max = value;
        }

        public static implicit operator MRange(float value) => new(value);
    }
}
