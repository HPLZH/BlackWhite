using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BW_Mobile
{
    public class BWButton : Button
    {
        protected (int X , int Y) location;
        public int LocationX => location.X;
        public int LocationY => location.Y;

        protected bool color;
        public bool Color
        {
            get => color;
            set
            {
                color = value;
                this.BackgroundColor = color ? Xamarin.Forms.Color.Black : Xamarin.Forms.Color.White;
            }
        }

        public BWButton(int X ,int Y ,EventHandler handler)
        {
            location = (X,Y);
            Color = false;
            this.BorderWidth = 1;
            this.BorderColor = Xamarin.Forms.Color.White;
            this.Clicked += handler;
        }

        public void ChangeColor()
        {
            Color = !color;
        }

        /*
        public void ChangeSize(double size)
        {
            this.WidthRequest = size;
            this.HeightRequest = size;
        }
        */
    }
}
