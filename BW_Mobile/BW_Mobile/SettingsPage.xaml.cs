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
            version.Text = $"{App.Version.ToString()}{(App.Preview ? " Preview":"")}";
            paltform.Text = Device.RuntimePlatform;
            releaseDate.Text = App.ReleaseDate;
            swTestMode.IsToggled = settings.TestMode;
            swPortraitOnly.IsToggled = settings.PortraitOnly;
            emptyLab.IsVisible = true;
            saveInfo.IsVisible = false;
        }

        private void credits_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreditsPage());
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
            if (Device.RuntimePlatform == Device.Android)
            {
                if (settings.PortraitOnly)
                {
                    DependencyService.Get<IOrientationService>().Portrait();
                }
                else
                {
                    DependencyService.Get<IOrientationService>().User();
                }
            }
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

        private void swPortraitOnly_Toggled(object sender, ToggledEventArgs e)
        {
            settings.PortraitOnly = swTestMode.IsToggled;
            emptyLab.IsVisible = false;
            saveInfo.IsVisible = true;
        }
    }
}