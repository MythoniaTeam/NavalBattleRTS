using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Game.Objects.Actions.Args
{
    public struct Numerical : IActionArg<Numerical>
    {
        private float _value;

        private float _rate = 0;
        public float Rate => _rate;

        public float Value => _value;

        Numerical IActionArg<Numerical>.MultipleRate(float multiplier)
        {
            _rate = multiplier;
            return new(Value * multiplier) { _rate = multiplier };
        }

        public Numerical(float value) => _value = value;

    }
}
