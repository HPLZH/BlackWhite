using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackWhite.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VersionPage : ContentPage
    {
        public VersionPage()
        {
            InitializeComponent();
            version.Text = AppVersion.Version.ToString();
            paltform.Text = Device.RuntimePlatform;
            releaseDate.Text = AppVersion.ReleaseDate.ToLongDateString();
        }
    }
}