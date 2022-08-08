using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App
{
    public static class AppVersion
    {
        public static Version Version => new Version($"{MainVersion}.{SubVersion}.{Build}");
        public const string MainVersion = "3";
        public const string SubVersion = "0";
        public const string Build = "17";

        public static DateTime ReleaseDate => new DateTime(2022, 8, 7);
    }
}
