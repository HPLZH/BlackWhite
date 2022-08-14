using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App.Properties
{
    internal static class StringTools
    {
        public static string Replace(string origin,params string[] replacements)
        {
            string result = origin;
            for(int i = 0; i < replacements.Length; i++)
            {
                result = result.Replace("{" + i.ToString() + "}", replacements[i]);
            }
            return result;
        }

        public static string MultiLine(params string[] strings)
        {
            if (strings == null || strings.Length == 0) return "";
            string result = strings[0];
            for(int i = 1;i< strings.Length; i++)
            {
                result += "\n" + strings[i];
            }
            return result.Trim();
        }
    }
}
