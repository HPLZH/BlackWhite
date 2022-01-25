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
        private Settings settings;
        private IReLoad parentPage;

        public SettingsPage(IReLoad parent)
        {
            InitializeComponent();
            settings = new Settings();
            parentPage = parent;
            version.Text = $"{App.Version.ToString()}{((App.DebugMode == "") ? "" : " ")}{App.DebugMode}";
            paltform.Text = Device.RuntimePlatform;
            releaseDate.Text = App.ReleaseDate;
            swTestMode.IsToggled = settings.TestMode;
            emptyLab.IsVisible = true;
            saveInfo.IsVisible = false;
        }

        private void developers_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DevelopersPage());
        }

        private void licence_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Licence());
        }

        private void save_Clicked(object sender, EventArgs e)
        {
            settings.SaveAll();
            saveInfo.IsVisible = false;
            emptyLab.IsVisible = true;
        }

        private void swTestMode_Toggled(object sender, ToggledEventArgs e)
        {
            settings.TestMode = swTestMode.IsToggled;
            emptyLab.IsVisible = false;
            saveInfo.IsVisible = true;
        }

        private void ContentPage_Disappearing(object sender, EventArgs e)
        {
            parentPage.ReLoad();
        }
    }
}