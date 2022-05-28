


namespace Mythonia.Resources.Debugger.Exceptions
{
    public class IncorrectTypeException : Exception
    {
        public IncorrectTypeException(string objDescription, object obj, params Type[] correctTypes) 
            : base($"{objDescription} has an Incorrect Type: \"{obj.GetType().Name}\" of Obj {obj},\nCorrect Types: {correctTypes.Cast<string>()}")
        {

        }
    }
}
