using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BlackWhite.Core;

namespace BlackWhite.MAUI
{
    public class BlockGrid : Grid
    {
        public Blocks<ButtonBlock> blocks;
        public bool Initialized { get; protected set; }

        public BlockGrid()
        {
            Initialized = false;
        }

        protected void Initialize(uint Xm, uint Ym)
        {
            if (Initialized)
            {
                return;
            }
            Initialized = true;
            blocks = new Blocks<ButtonBlock>(Xm, Ym);
            for(uint ix = 0; ix < blocks.Xmax; ix++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (uint iy = 0; iy < blocks.Ymax; iy++)
            {
                RowDefinitions.Add(new RowDefinition());
            }
            for(uint ix = 0; ix < blocks.Xmax; ix++)
            {
                for(uint iy = 0; iy < blocks.Ymax; iy++)
                {
                    blocks[ix, iy] = new ButtonBlock();
                    Children.Add(blocks.GetBlock(ix,iy), (int)ix, (int)iy);
                    blocks.GetBlock(ix, iy).Value = false;
                    blocks.GetBlock(ix, iy).Clicked += OnClick;
                }
            }
        }

        protected void OnClick(object sender,EventArgs eventArgs)
        {
            blocks.Click((ButtonBlock)sender);
            ButtonClicked(this,new ButtonClickedEventArgs((ButtonBlock)sender));
        }

        public event EventHandler<ButtonClickedEventArgs> ButtonClicked;

        public class ButtonClickedEventArgs : EventArgs
        {
            public readonly ButtonBlock button;

            public ButtonClickedEventArgs(ButtonBlock buttonBlock)
            {
                button = buttonBlock;
            }
        }
    }
}
