using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackWhite.App
{
    public partial class App : Application
    {
        public App()
        {
            //App.Current.Properties.Clear();

            InitializeComponent();

            ChangeLanguage();

            MainPage = new NavigationPage(new MainPage());
        }

        public static void ChangeLanguage()
        {
            System.Globalization.CultureInfo culture;
            if (Settings.Language == null)
            {
                culture = System.Globalization.CultureInfo.CurrentCulture;
            }
            else
            {
                culture = new System.Globalization.CultureInfo(Settings.Language);
            }

            System.Globalization.CultureInfo.CurrentUICulture = culture;
            
            BlackWhite.App.Properties.App.Culture = culture;
            BlackWhite.App.Properties.MainPage.Culture = culture;
            BlackWhite.App.Properties.VersionPage.Culture = culture;
            BlackWhite.App.Properties.StartPage.Culture = culture;
            BlackWhite.App.Properties.GamePage.Culture = culture;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
