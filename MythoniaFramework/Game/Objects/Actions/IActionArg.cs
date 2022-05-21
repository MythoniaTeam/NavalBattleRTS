using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Game.Objects.Actions
{
    public interface IActionArg<T>
    {
        public T MultipleRate(float multiplier);

        public float Rate { get; }


    }
}
