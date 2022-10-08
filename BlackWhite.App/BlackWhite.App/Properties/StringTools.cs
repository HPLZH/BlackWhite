using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App.Properties
{
    internal static class StringTools
    {
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
