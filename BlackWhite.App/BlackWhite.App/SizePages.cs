using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.App
{
    internal class NormalSizePage : SizePage
    {
        public NormalSizePage()
            : base(Properties.StartPage.Normal, Properties.StartPage.Normal_Text) { }

        protected override void StartGame(int size)
        {
            if(size == -1)
            {
                Navigation.PushAsync(new NormalGamePage(new Random()));
            }
            else
            {
                Navigation.PushAsync(new NormalGamePage(size));
            }
            base.StartGame(size);
        }
    }

    internal class PerfectSizePage : SizePage
    {
        public PerfectSizePage()
            :base(Properties.StartPage.Perfect, Properties.StartPage.Perfect_Text) { }

        protected override void StartGame(int size)
        {
            if (size == -1)
            {
                Navigation.PushAsync(new PerfectGamePage(new Random()));
            }
            else
            {
                Navigation.PushAsync(new PerfectGamePage(size));
            }
            base.StartGame(size);
        }
    }
}
