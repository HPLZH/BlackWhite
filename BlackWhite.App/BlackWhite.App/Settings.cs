using System;
using System.Collections.Generic;
using System.Text;
using static Xamarin.Forms.Application;

namespace BlackWhite.App
{
    internal static class Settings
    {
        const string SETTINGS = "Settings";

        //语言
        const string LANGUAGE = "Language";
        public static string Language
        {
            get => PropertiesIO.Get<string>(SETTINGS, LANGUAGE);
            set => PropertiesIO.Set<string>(value, SETTINGS, LANGUAGE);
        }
    }
}
