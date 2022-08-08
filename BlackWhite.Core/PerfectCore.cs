using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    public class PerfectCore<T> : GameCore<T>
    {
        public uint Remaining { get => blocks1.Xmax * blocks1.Ymax - Count; }

        public override bool Click(uint x,uint y)
        {
            if (base.Click(x, y))
            {
                return true;
            }
            else if (Remaining == 0)
            {
                Stop();
            }
            return false;
        }

        protected void Stop()
        {
            State = States.Stopped;
        }
    }
}
