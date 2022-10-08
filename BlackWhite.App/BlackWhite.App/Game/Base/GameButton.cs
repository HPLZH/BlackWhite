using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using BlackWhite.Core;

namespace BlackWhite.App.Game.Base
{
    public class GameButton : Button, IBlock<GameButton>
    {
        public GameButton()
            : base()
        {
            BorderColor = Color.Gray;
            BorderWidth = 2;
            Clicked += (sender, e) => { BlockClicked(this, new BlockClickedEventArgs(X, Y)); };
        }

        public event EventHandler<BlockClickedEventArgs> BlockClicked;

        bool _value;
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                BackgroundColor = _value ? Color.White : Color.Black;
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
