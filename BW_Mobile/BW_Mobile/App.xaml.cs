using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BW_Mobile
{
    public partial class App : Application
    {
        public static Version Version => new Version(2, 0, 0, 2);
        public static string DebugMode => "";
        public static string ReleaseDate => "2021年10月1日";

        public App()
        {
            InitializeComponent();

            MainPage = MainPage = new NavigationPage(new MainPage());
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
