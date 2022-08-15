using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    public class PerfectCore<T> : GameCore<T>
        where T : IBlock<T>, new()
    {
        public uint Remaining { get => blocks1.Xmax * blocks1.Ymax - Count; }

        public event EventHandler GameStopped;

        public PerfectCore() { }

        public PerfectCore(Blocks<T> blocks,Random random)
            :base(blocks, random) { }

        protected override bool Clicked()
        {
            if(base.Clicked())
            {
                if (Remaining == 0)
                {
                    Stop();
                    return false;
                }
                else return true;
            }
            else return false;
        }

        protected void Stop()
        {
            State = States.Stopped;
            GameStopped(this, EventArgs.Empty);
        }
    }
}
