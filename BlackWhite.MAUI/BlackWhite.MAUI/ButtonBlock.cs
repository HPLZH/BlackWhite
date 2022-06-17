using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BlackWhite.Core;

namespace BlackWhite.MAUI
{
    public class ButtonBlock : Button , IBlock<ButtonBlock>  
    {
        public new uint X { get; set; }
        public new uint Y { get; set; }

        protected bool blockValue;

        public bool Value
        {
            get => blockValue;
            set
            {
                blockValue = value;
                BackgroundColor = blockValue ? Color.Black : Color.White;
            }
        }
    }
}
