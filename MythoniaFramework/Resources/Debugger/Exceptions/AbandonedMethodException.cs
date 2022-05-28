


namespace Mythonia.Resources.Debugger.Exceptions
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
