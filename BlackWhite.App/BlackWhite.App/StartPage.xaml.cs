﻿using System;
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
			//Navigation.PushAsync(new SizePage(Properties.StartPage.Normal, Properties.StartPage.Normal_Text));
		}

		private void Timed_ButtonClicked(object sender, EventArgs e)
		{
			//Navigation.PushAsync(new SizePage(Properties.StartPage.Normal, Properties.StartPage.Normal_Text));
		}

		private void Free_ButtonClicked(object sender, EventArgs e)
		{

		}

    }
}