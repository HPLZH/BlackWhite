using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackWhite.App.Game.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackWhite.App.Game.Select
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SizePage : ContentPage
	{
		protected Button[] buttons;

		public SizePage (string title, string text)
		{
			InitializeComponent();
            titleLabel.Text = title;
			textLabel.Text = text;
			buttons = new Button[] { s3, s4, s5, s6, s7, s8, s9 };
		}

        protected override void OnSizeAllocated(double width, double height)
        {
            //int maxSize = SizeCalculator.GetMax();
            int maxSize = SizeCalculator.GetMaxSize(SizeCalculator.GetTotalSize(width, height));
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].IsEnabled = i + 3 <= maxSize;
            }
            base.OnSizeAllocated(width, height);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int size = 0;
			for(int i = 0; i < buttons.Length; i++)
            {
                if(sender == buttons[i])
                {
                    size = i + 3;
                    break;
                }
            }
            StartGame(size);
        }

        protected virtual void StartGame(int size) { }

        private void random_Clicked(object sender, EventArgs e)
        {
            StartGame(-1);
        }
    }
}