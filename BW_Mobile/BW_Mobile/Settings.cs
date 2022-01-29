using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace BW_Mobile
{
    class Settings
    {
        private PropertiesIO<bool> settingsBool;
        private PropertiesIO<string> settingsString;

        public Settings()
        {
            settingsBool = new PropertiesIO<bool>();
            settingsString = new PropertiesIO<string>();
            Refresh();
        }

        public void Refresh()
        {
            testMode = settingsBool.InitGet("TestMode", false);
            portraitOnly = settingsBool.InitGet("PortraitOnly", (Device.RuntimePlatform == Device.Android) && (Device.Idiom == TargetIdiom.Phone));
            language = settingsString.InitGet("Language", "%null%");
        }

        public void SaveAll()
        {
            settingsBool["TestMode"] = testMode;
            settingsBool["PortraitOnly"] = portraitOnly;
            settingsString["Language"] = language;
        }

        private bool testMode;
        /// <summary>
        /// 测试模式
        /// </summary>
        public bool TestMode
        {
            get { return testMode; }
            set { testMode = value; }
        }

        private bool portraitOnly;
        /// <summary>
        /// 竖屏锁定
        /// </summary>
        public bool PortraitOnly
        {
            get { return portraitOnly; }
            set { portraitOnly = value; }
        }

        private string language;
        /// <summary>
        /// 语言
        /// </summary>
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

    }
}
