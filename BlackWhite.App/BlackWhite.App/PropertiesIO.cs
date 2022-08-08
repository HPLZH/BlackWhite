using System;
using System.Collections.Generic;
using System.Text;
using static Xamarin.Forms.Application;

namespace BlackWhite.App
{
    internal static class PropertiesIO
    {
        public static string FullName(params string[] parts)
        {
            string fullName = "";
            foreach (string part in parts)
            {
                fullName += $"-{part}";
            }
            return fullName.Substring(1);
        }

        public static T Get<T>(params string[] keys)
        {
            if (Current.Properties.ContainsKey(FullName(keys)))
            {
                return (T)Current.Properties[FullName(keys)];
            }
            else
            {
                return default(T);
            }
        }

        public static T GetWithDefault<T>(T defaultValue,params string[] keys)
        {
            if (Current.Properties.ContainsKey(FullName(keys)))
            {
                return (T)Current.Properties[FullName(keys)];
            }
            else
            {
                return defaultValue;
            }
        }

        public static void Set<T>(T value,params string[] keys)
        {
            Current.Properties[FullName(keys)] = value;
        }
    }
}
