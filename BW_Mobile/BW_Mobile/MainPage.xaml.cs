using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BW_Mobile
{
    public partial class MainPage : ContentPage , IReLoad
    {
        Settings setting;

        public MainPage()
        {
            InitializeComponent();
            setting = new Settings();
            if (Device.Idiom == TargetIdiom.Phone && Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IOrientationService>().Portrait();
            }
            if (App.DebugMode != "")
            {
                Title = App.Version.ToString() + " " + App.DebugMode;
            }
            ReLoad();
        }

        public void ReLoad()
        {
            setting.Refresh();
            debug.IsVisible = setting.TestMode;
        }

        protected int orientation; // 0=>unexpected 1=>V -1=>H
        protected override void OnSizeAllocated(double width, double height)
        {
            int orient;
            if (width / height > 1.25)
            {
                orient = -1;
            }else if(width / height < 0.8)
            {
                orient = 1;
            }
            else
            {
                orient = 0;
            }
            if(orient != orientation)
            {
                orientation = orient;
                if (Device.Idiom != TargetIdiom.Desktop)
                {
                    switch (orientation)
                    {
                        case 1:
                        case 0:
                            tips.IsVisible=false;
                            break;
                        case -1:
                            tips.IsVisible=true;
                            break;
                    }
                }
            }
            base.OnSizeAllocated(width, height); //must be called
        }

        private void start_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChooseMode());
        }

        private void debug_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DebugPage());
        }

        private void settings_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SettingsPage(this));
        }

        private void help_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HelpPage());
        }
    }
}
