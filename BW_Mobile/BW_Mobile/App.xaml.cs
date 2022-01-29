using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BW_Mobile
{
    public partial class App : Application , IReLoad
    {
        public static Version Version => new Version(2, 3, 14);
        public static bool Preview => true;
        public static string ReleaseDate => new DateTime(2022,1,29).ToLongDateString();

        /// <summary>
        /// 用输入的参数替换原字符串中的 %_% ,中间的数字(序号)表示对应的参数的位置(从1开始), %0% 表示 %
        /// </summary>
        /// <param name="origin">原始字符串</param>
        /// <param name="args">输入的参数</param>
        /// <returns>返回一个被替换后的字符串,序号超出范围的将不会被替换</returns>
        public static string Replace(string origin, params string[] args)
        {
            string result = origin;
            for(int i = 1; i <= args.Length; i++)
            {
                result=result.Replace($"%{i}%", args[i-1]);
            }
            result = result.Replace("%0%", "%");
            return result;
        }

        private Settings settings;

        public App()
        {
            InitializeComponent();
            //Properties.Clear();
            settings = new Settings();

            ReLoad();
        }

        public void ReLoad()
        {
            settings.Refresh();
            bool askLang = false;
            switch (settings.Language)
            {
                case "%null%":
                    askLang = true;
                    break;
                case "%system%":
                    BW_Mobile.Properties.Resources.Culture = null;
                    break;
                default:
                    BW_Mobile.Properties.Resources.Culture=System.Globalization.CultureInfo.GetCultureInfo(settings.Language);
                    break;
            }
            //askLang = true;
            if (settings.PortraitOnly)
            {
                DependencyService.Get<IOrientationService>().Portrait();
            }
            MainPage = new NavigationPage(new MainPage(this, askLang));
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
