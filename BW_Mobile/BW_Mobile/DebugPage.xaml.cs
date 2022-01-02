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
    public partial class DebugPage : ContentPage
    {
        public DebugPage()
        {
            InitializeComponent();
            version.Text = App.Version.ToString() + " " + App.DebugMode; // + Environment.NewLine + "Build " + Environment.Version.Build;
            paltform.Text = Device.RuntimePlatform;
            idiom.Text = Device.Idiom.ToString();
            if(Device.RuntimePlatform == Device.Android)
            {
                landspaceMode.IsVisible = true;
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    //landspace.IsVisible = true;
                    //onAndOff.IsVisible = true;
                    //bLandspace.IsVisible = true;
                    //bPortrait.IsVisible = true;
                    butsLandspace.IsVisible = true;
                }
                else
                {
                    dependOnDevice.IsVisible = true;
                }
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            size.Text = "MainPage: " + Math.Round(Application.Current.MainPage.Width).ToString() + "*" + Math.Round(Application.Current.MainPage.Height).ToString() + Environment.NewLine + "Current:  " + Math.Round(width).ToString() + "*" + Math.Round(height).ToString();
            base.OnSizeAllocated(width, height); //must be called
        }

        private void landspace_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                DependencyService.Get<IOrientationService>().Landscape();
            }
            else
            {
                DependencyService.Get<IOrientationService>().Portrait();
            }
        }

        private void bLandspace_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationService>().Landscape();
        }

        private void bPortrait_Clicked(object sender, EventArgs e)
        {
            DependencyService.Get<IOrientationService>().Portrait();
        }
    }
}