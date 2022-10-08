using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using BlackWhite.App.Game.Select;

namespace BlackWhite.App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            //System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");

            InitializeComponent();

            languageTextChange = new Thread(LanguageTextChange);
            languageTextChange.Start();
        }

        private (double width, double height) windowSize;
        public (double width,double height) WindowSize
        {
            get { return windowSize; }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            windowSize = (width, height);
            base.OnSizeAllocated(width, height);
        }

        private void Button_Clicked_Version(object sender, EventArgs e)
        {
            Navigation.PushAsync(new VersionPage());
        }

        private (string Name, string Tag, string ChooseText)[] languageChoose =
        {
            ("简体中文","zh-CN","语言"),
            ("English","en","Language")
        };

        private string[] LanguageStrings()
        {
            string[] strings = new string[languageChoose.Length];
            for(int i = 0; i < languageChoose.Length; i++)
            {
                strings[i] = languageChoose[i].Name;
            }
            return strings;
        }

        private Thread languageTextChange;
        private void LanguageTextChange()
        {
            //Thread.Sleep(TimeSpan.FromSeconds(2));
            Thread.Sleep(2000);
            foreach(var language in languageChoose)
            {
                Dispatcher.BeginInvokeOnMainThread(() => languageButton.Text = language.ChooseText);
                //Thread.Sleep(TimeSpan.FromSeconds(1.5));
                Thread.Sleep(2000);
            }
            Dispatcher.BeginInvokeOnMainThread(() => languageButton.Text = BlackWhite.App.Properties.MainPage.Language);       
        }

        private async void Button_Clicked_Language(object sender, EventArgs e)
        {
            string targetLanguage = await DisplayActionSheet(BlackWhite.App.Properties.MainPage.LanguageChoose, null, null, buttons: LanguageStrings());
            string targetLanguageTag = null;
            foreach(var language in languageChoose)
            {
                if (language.Name == targetLanguage)
                {
                    targetLanguageTag = language.Tag;
                    break;
                }
            }
            if (targetLanguageTag != null)
            {
                Settings.Language = targetLanguageTag;
                await Application.Current.SavePropertiesAsync();
                App.ChangeLanguage();
                App.Current.MainPage = new NavigationPage(new MainPage());
                
            }

        }

        private void Button_Clicked_Start(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StartPage());
        }

        private void Button_Clicked_Statistics(object sender, EventArgs e)
        {
            Navigation.PushAsync(new StatisticsPage());
        }
    }
}
