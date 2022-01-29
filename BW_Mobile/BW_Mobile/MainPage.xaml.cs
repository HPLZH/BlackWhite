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
        IReLoad parentApp;
        bool askLang;

        public MainPage(IReLoad parent, bool askLanguage)
        {
            //Properties.Resources.Culture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
            parentApp = parent;
            //start.Text = Properties.Resources.MainPage_Start;
           
            setting = new Settings();
            if (Device.Idiom == TargetIdiom.Phone && Device.RuntimePlatform == Device.Android)
            {
                DependencyService.Get<IOrientationService>().Portrait();
            }
            if (App.Preview)
            {
                Title = App.Version.ToString() + " Preview";
            }
            ReLoad();
            askLang = askLanguage;
            
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

        private async void lang_Clicked(object sender, EventArgs e)
        {
            string langResult = await DisplayActionSheet(Properties.Resources.MainPage_Language,askLang ? null : Properties.Resources.MsgBox_Cancel, Properties.Resources.MainPage_Language_System, "简体中文", "English");
            string langFinalResult = "%system%";
            if (langResult == Properties.Resources.MsgBox_Cancel)
            {
                return;
            }
            askLang = false;
            switch (langResult)
            {
                case "简体中文":
                    langFinalResult = "zh-CN";
                    break;
                case "English":
                    langFinalResult = "en";
                    break;
                default:
                    break;
            }
            setting.Language = langFinalResult;
            setting.SaveAll();
            parentApp.ReLoad();
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (askLang)
            {
                lang_Clicked(this, null);
            }
        }
    }
}
