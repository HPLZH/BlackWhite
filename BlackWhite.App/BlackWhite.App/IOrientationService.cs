using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App
{
    public interface IOrientationService
    {
        void Landscape();
        void Portrait();
        void User();
    }
}
