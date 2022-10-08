using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    public class Blocks<T> : IEnumerable<T>
        where T : IBlock<T>, new()
    {
        protected IBlock<T>[,] blocks;

        public uint Xmax { get; protected set; }
        public uint Ymax { get; protected set; }

        public bool DefaultChange { get; set; }

        public Blocks(uint Xm, uint Ym)
        {
            Xmax = Xm;
            Ymax = Ym;
            blocks = new IBlock<T>[Xm, Ym];
            for(uint ix = 0; ix < Xm; ix++)
            {
                for(uint iy = 0; iy < Ym; iy++)
                {
                    blocks[ix, iy] = new T() { Parent = this, X = ix, Y = iy };
                    blocks[ix, iy].BlockClicked += (object sender, BlockClickedEventArgs e) => { if (DefaultChange) Change(e.X, e.Y); else Click(e.X, e.Y); };
                }
            }
        }

        public IBlock<T> this[uint x,uint y]
        {
            get
            {
                return blocks[x,y];
            }
        }

        public T GetBlock(uint x,uint y)
        {
            return (T)blocks[x,y];
        }

        public void Check(IBlock<T> block)
        {
            if (!block.Equals(blocks[block.X, block.Y])) 
            {
                throw new Exception("对象与坐标不一致");
            }
        }

        public bool TryGetBlock(uint x, uint y, ref IBlock<T> returnBlock)
        {
            if (x < Xmax && y < Ymax) 
            {
                returnBlock = blocks[x, y];
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Change(uint x,uint y)
        {
            blocks[x, y].Value = !blocks[x, y].Value;
        }

        public bool TryChange(uint x, uint y)
        {
            IBlock<T> block = null;
            if(TryGetBlock(x, y, ref block))
            {
                block.Value = !block.Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Click(uint x, uint y)
        {
            Change(x, y);
            TryChange(x + 1, y);
            TryChange(x - 1, y);
            TryChange(x, y + 1);
            TryChange(x, y - 1);
            BlockClicked(this, new BlockClickedEventArgs(x, y));
        }

        public event EventHandler<BlockClickedEventArgs> BlockClicked;

        public bool IsSame(bool target)
        {
            foreach (IBlock<T> block in blocks)
            {
                if (block.Value != target)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSame()
        {
            return IsSame(blocks[0, 0].Value);
        }

        public void Clear(bool value = true)
        {
            foreach (IBlock<T> block in blocks)
            {
                block.Value = value;
            }
        }

        public void FullRandomize(Random random)
        {
            foreach (IBlock<T> block in blocks)
            {
                block.Value = random.Next(2) == 1;
            }
        }

        public void MixRandomize(Random random)
        {
            for (uint ix = 0; ix < Xmax; ix++)
            {
                for (uint iy = 0; iy < Ymax; iy++)
                {
                    if (random.Next(2) == 1)
                    {
                        Click(ix, iy);
                    }
                }
            }
        }

        public void GameRandomize(Random random)
        {
            Clear();
            MixRandomize(random);
            if (IsSame())
            {
                GameRandomize(random);
            }
        }

        public void Invert()
        {
            foreach (IBlock<T> block in blocks) block.Value = !block.Value;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator<T>(this);
        }

        class Enumerator<T1> : IEnumerator<T1>
            where T1 : IBlock<T1>, new()
        {
            private bool disposedValue;
            private uint curX;
            private uint curY;
            private bool first;

            private Blocks<T1> blocks;
            public Enumerator(Blocks<T1> blocks1)
            {
                blocks = blocks1;
                Reset();
            }

            public T1 Current => blocks.GetBlock(curX, curY);

            object IEnumerator.Current => blocks.GetBlock(curX, curY);

            public bool MoveNext()
            {
                if (first)
                {
                    first = false;
                    return true;
                }
                curX++;
                if (curX == blocks.Xmax)
                {
                    curX = 0;
                    curY++;
                }
                if(curY >= blocks.Ymax)
                {
                    curX = 0;
                    curY = blocks.Ymax;
                    return false;
                }
                return true;
            }

            public void Reset()
            {
                first = true;
                curX = 0;
                curY = 0;
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposedValue)
                {
                    if (disposing)
                    {
                        // TODO: 释放托管状态(托管对象)
                    }

                    // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                    // TODO: 将大型字段设置为 null
                    blocks = null;
                    disposedValue = true;
                }
            }

            // // TODO: 仅当“Dispose(bool disposing)”拥有用于释放未托管资源的代码时才替代终结器
            // ~Enumerator()
            // {
            //     // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            //     Dispose(disposing: false);
            // }

            public void Dispose()
            {
                // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
