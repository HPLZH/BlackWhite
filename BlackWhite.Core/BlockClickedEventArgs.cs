using System;

namespace BlackWhite.Core
{
    public class BlockClickedEventArgs : EventArgs
    {
        public BlockClickedEventArgs(uint x, uint y)
            : base()
        {
            X = x;
            Y = y;
        }

        public uint X, Y;
    }
}
