using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Objects.Actions
{
    public class TestAction : ContinousAction<float>
    {
        public TestAction(float duration, float moveDistance) : base(duration, moveDistance, MainAction) { }

        private static bool MainAction(float args, float multiplier)
        {
            return true;
        }
    }
}
