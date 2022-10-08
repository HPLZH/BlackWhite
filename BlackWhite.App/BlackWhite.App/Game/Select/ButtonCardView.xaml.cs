using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackWhite.App.Game.Select
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ButtonCardView : ContentView
	{
		public ButtonCardView ()
		{
			InitializeComponent ();
		}

		public string Title
        {
			get => titleLabel.Text;
			set => titleLabel.Text = value;
        }

		public string Text
        {
			get => textLabel.Text;
			set => textLabel.Text = value;
        }

		public string ButtonText
        {
			get => button.Text;
			set => button.Text = value;
        }

		public event EventHandler ButtonClicked;

        private void button_Clicked(object sender, EventArgs e)
        {
			ButtonClicked(sender, e);
        }
    }
}