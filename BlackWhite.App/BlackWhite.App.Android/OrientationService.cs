using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlackWhite.App;
using BlackWhite.App.Droid;
using Xamarin.Forms;
using Android.Content.PM;

[assembly: Xamarin.Forms.Dependency(typeof(OrientationService))]
namespace BlackWhite.App.Droid
{
    public class OrientationService : IOrientationService
    {
        [Obsolete]
        public void Landscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        [Obsolete]
        public void Portrait()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Portrait;
        }

        [Obsolete]
        public void User()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.User;
        }
    }
}