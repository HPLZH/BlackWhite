using System;
using System.Collections.Generic;
using System.Text;

namespace BW_Mobile
{
    class Settings
    {
        private PropertiesIO<bool> settingsBool;


        public Settings()
        {
            settingsBool = new PropertiesIO<bool>();
            Refresh();
        }

        public void Refresh()
        {
            testMode = settingsBool.InitGet("TestMode", false);
        }

        public void SaveAll()
        {
            settingsBool["TestMode"] = testMode;
        }

        //测试模式
        private bool testMode;
        public bool TestMode
        {
            get { return testMode; }
            set { testMode = value; }
        }


    }
}
