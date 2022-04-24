using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Debugger.Exceptions
{
    public class AbandonedMethodException : Exception
    {
        public AbandonedMethodException(string methodName, string inheritedClass = null) : base(
            $"An Abandoned Method \"{methodName}\"" +
            ((inheritedClass is null) ? "" : $" (inherited form class \"{inheritedClass}\")") + 
             " was Called")
        {

        }
    }
}
