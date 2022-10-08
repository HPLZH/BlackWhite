using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace BlackWhite.App.Game.Select
{
    internal class NumberButton : Button
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                this.Text = _value.ToString();
            }
        }
    }
}
