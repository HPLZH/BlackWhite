using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackWhite.App
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();
		}

        private void Normal_ButtonClicked(object sender, EventArgs e)
        {
			Navigation.PushAsync(new NormalSizePage());
        }

		private void Perfect_ButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new PerfectSizePage());
		}

		private void Timed_ButtonClicked(object sender, EventArgs e)
		{
			Navigation.PushAsync(new TimedSizePage());
		}

		private void Free_ButtonClicked(object sender, EventArgs e)
		{

		}

    }
}