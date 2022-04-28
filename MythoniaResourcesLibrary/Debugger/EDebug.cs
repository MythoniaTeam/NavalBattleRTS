using System;
using System.Collections.Generic;
using System.Text;



namespace Mythonia.Resources.Debugger
{
    public static class EDebug
    {
        public static Dictionary<object, Type> TypeDic = new Dictionary<object, Type>();
        public static void Log(this object v, bool activate, string type, string info)
        {
#if DEBUG
            if (!activate) return;
            Debug.WriteLine("#" + type.PadRight(16) + "#\t " +
                GetName(v, " \"{0}\" ").PadRight(8) +
                $"\t {info}");
#endif
        }
        public static void Log(this object v, bool activate, string info)
        {
#if DEBUG
            if (!TypeDic.ContainsKey(v))
                TypeDic.Add(v, v.GetType());

            Type type = TypeDic[v];
            Log(type, activate, type.Name, info);
#endif
        }


        private static string GetName(object obj, string str = "{0}") => (obj is INamed) ? string.Format(str, ((INamed)obj).Name) : "";

        public static void LogConstruct(this object v, bool activate, string type)
        {
            Debug.WriteLine($"*Constructing*   " + $"new [{type}],".PadRight(20) + $"{GetName(v, " named \"{0}\"")}" + " (from Namespace {v.GetType().Namespace})");
        }
        public static void LogConstruct(this object v, bool activate)
        {
#if DEBUG
            if (!TypeDic.ContainsKey(v))
                TypeDic.Add(v, v.GetType());
            LogConstruct(v, activate, TypeDic[v].Name);
#endif
        }
    }
}
