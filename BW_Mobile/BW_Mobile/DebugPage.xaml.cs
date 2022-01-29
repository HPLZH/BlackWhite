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
            version.Text = App.Version.ToString() + (App.Preview ? " Preview" : ""); // + Environment.NewLine + "Build " + Environment.Version.Build;
            paltform.Text = Device.RuntimePlatform;
            idiom.Text = Device.Idiom.ToString();
            //Properties.Resources.Culture = System.Globalization.CultureInfo.CurrentCulture;
            if(Properties.Resources.Culture != null)
            {
                langResources.Text = $"Resources:[{Properties.Resources.Culture.Name}]{Properties.Resources.Culture.DisplayName}";
            }
            else
            {
                langResources.Text = "Resources:[null]";
            }
            langCurrent.Text = $"Current:[{System.Globalization.CultureInfo.CurrentCulture.Name}]{System.Globalization.CultureInfo.CurrentCulture.DisplayName}";
            langCurrentUI.Text = $"CurrentUI:[{System.Globalization.CultureInfo.CurrentCulture.Name}]{System.Globalization.CultureInfo.CurrentCulture.DisplayName}";
            
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            size.Text = "MainPage: " + Math.Round(Application.Current.MainPage.Width).ToString() + "*" + Math.Round(Application.Current.MainPage.Height).ToString() + Environment.NewLine + "Current:  " + Math.Round(width).ToString() + "*" + Math.Round(height).ToString();
            base.OnSizeAllocated(width, height); //must be called
        }
    }
}