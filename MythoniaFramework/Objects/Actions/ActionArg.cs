using System;
using System.Collections.Generic;
using System.Text;

namespace Mythonia.Framework.Objects.Actions
{
    public interface IActionArgPolicy<T>
    {
        public T Multiple(T arg, float multiplier);
    }

    public class ActionArg
    {
        public static bool IsValidActionArg<T>(T arg)
        {
            return arg switch
            {
                float => true,
                IActionArg<T> => true,
                _ => false,
            };
        }


        

    }

    public static class EActionArg
    {
        public static T ArgMultiple<T>(this IActionArg<T> arg, float multiplier) => arg.Multiple(multiplier);
        public static float ArgMultiple(this float arg, float multiplier) => arg * multiplier;
    }

    public class InvalidActionArgTypeException : Exception
    {
        public InvalidActionArgTypeException() : base() { }
        public InvalidActionArgTypeException(string extraInfo, Type argType) : base($"Invalid Action Arg Type: {argType}, {extraInfo}") { }
    }

    
}
