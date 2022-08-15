using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App
{
    public static class AppVersion
    {
        public static Version Version => new Version(3,0,18);

        public static DateTime ReleaseDate => new DateTime(2022, 8, 15);

        public static bool Preview => true;
    }
}
