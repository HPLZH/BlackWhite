using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App
{
    public static class AppVersion
    {
        public static Version Version => new Version(3,1,21);

        public static DateTime ReleaseDate => new DateTime(2022, 9, 2);

        public static bool Preview => false;
    }
}
