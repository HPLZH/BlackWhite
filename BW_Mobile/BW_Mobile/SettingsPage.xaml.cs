using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BW_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            version.Text = $"{App.Version.ToString()}{((App.DebugMode == "") ? "" : " ")}{App.DebugMode}";
            paltform.Text = Device.RuntimePlatform;
            releaseDate.Text = App.ReleaseDate;
        }

        private void developers_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DevelopersPage());
        }

        private void licence_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Licence());
        }
    }
}