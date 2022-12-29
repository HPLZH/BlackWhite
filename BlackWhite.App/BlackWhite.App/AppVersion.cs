using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App
{
    public static class AppVersion
    {
        public static Version Version => new Version(3, 2, 24);

        public static DateTime ReleaseDate => new DateTime(2022, 12, 29);

        public static bool Preview => false;
    }
}
