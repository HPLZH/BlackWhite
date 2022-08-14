using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace BlackWhite.App
{
    public static class SizeCalculator
    {
        //padding=0

        public static MainPage GetMainPage() => (MainPage)((NavigationPage)App.Current.MainPage).RootPage;

        public static double GetTotal() => GetTotalSize(GetMainPage().WindowSize.width, GetMainPage().WindowSize.height);
        public static int GetMax() => GetMaxSize(GetTotal());
        public static double GetBlock(int size) => GetBlockSize(GetTotal(), size);
        

        public const double LAYOUT_PADDING = 20;
        public const double SPACING = 5;
        public const double MIN_BLOCK = 30;
        public static int GetMaxSize(double total) => (int)Math.Floor((total - LAYOUT_PADDING * 2 + SPACING) / (MIN_BLOCK + SPACING));
        public static double GetBlockSize(double total, int size) => (total - LAYOUT_PADDING * 2 + SPACING) / size - SPACING;
        public static double GetTotalSize(double width, double height) => width > height ? height : width;
        public static bool IsSupported(double total, int size) => total >= GetBlockSize(total, size);
        public static double GetFrameSize(double width, double height)
        {
            double r = width > height ? width / height : height / width;
            if (r >= 5.0 / 3.0)
            {
                return Math.Abs(height - width) / 2;
            }
            else if (r >= 4.0 / 3.0)
            {
                return GetTotalSize(width, height) / 3;
            }
            else
            {
                return Math.Abs(height - width);
            }
        }
    }
}
