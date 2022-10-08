using BlackWhite.App.Game.Base;
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
    public partial class NumberSizePage : ContentPage
    {
        private int _size = 0;

        protected virtual void StartGame(int size) { }

        public NumberSizePage(string title, string text)
        {
            InitializeComponent();
            titleLabel.Text = title;
            textLabel.Text = text;
            ChangeValue(0);
        }

        private void ac_Clicked(object sender, EventArgs e)
        {
            ChangeValue(0);
        }

        private void NumberButton_Clicked(object sender, EventArgs e)
        {
            ChangeValue(_size * 10 + ((NumberButton)sender).Value);
        }

        private void start_Clicked(object sender, EventArgs e)
        {
            StartGame(_size);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            ChangeValue(_size);
            base.OnSizeAllocated(width, height);
        }

        private void ChangeValue(int value)
        {
            _size = value;
            sizeText.Text = value.ToString();
            if (value < 3)
            {
                sizeText.TextColor = Color.Red;
                start.IsEnabled = false;
            }
            else if (value > SizeCalculator.GetMax())
            {
                sizeText.TextColor = Color.Orange;
                start.IsEnabled = true;
            }
            else
            {
                sizeText.TextColor = Color.Green;
                start.IsEnabled = true;
            }
        }
    }
}