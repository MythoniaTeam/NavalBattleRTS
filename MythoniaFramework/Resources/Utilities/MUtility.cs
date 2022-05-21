using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Utilities
{
    public class MUtility
    {
        public static void Revert<t>(ref t a, ref t b)
        {
            t c = a;
            b = a;
            a = c;
        }
    }
}
