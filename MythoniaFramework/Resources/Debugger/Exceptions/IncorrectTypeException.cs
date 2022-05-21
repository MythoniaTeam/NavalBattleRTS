using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Resources.Debugger.Exceptions
{
    public class IncorrectTypeException : Exception
    {
        public IncorrectTypeException(string objDescription, object obj, Type[] correctTypes = null) 
            : base($"{objDescription} has an Incorrect Type: \"{obj.GetType().Name}\" of Obj {obj},\nCorrect Types: {correctTypes}")
        {

        }
    }
}
