using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BW_Mobile
{
    public partial class App : Application
    {
        public static Version Version => new Version(2, 3, 13);
        public static string DebugMode => "";
        public static string ReleaseDate => "2022年1月25日";

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
