using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Debugger.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string objType, string objName) : base(
            $"{objType} \"{objName}\" is Not Found")
        {

        }
    }
}
