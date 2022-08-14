using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BlackWhite.Core;

namespace BlackWhite.App
{
    public class GameButton : Button, IBlock<GameButton>
    {
        public GameButton()
            : base()
        {
            this.BorderColor = Color.Gray;
            this.BorderWidth = 2;
            this.Clicked += (object sender, EventArgs e) => { BlockClicked(this, new BlockClickedEventArgs(X, Y)); };
        }

        public event EventHandler<BlockClickedEventArgs> BlockClicked;

        bool _value;
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                this.BackgroundColor = _value ? Color.White : Color.Black;
            }
        }

        public new uint X { get; set; }
        public new uint Y { get; set; }

        Blocks<GameButton> _blocks = null;
        public new Blocks<GameButton> Parent
        {
            get => _blocks;
            set
            {
                _blocks = value;
            }
        }
    }
}
