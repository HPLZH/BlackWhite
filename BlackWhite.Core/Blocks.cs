using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    public class Blocks<T>
    {
        protected IBlock<T>[,] blocks;

        public uint Xmax { get; protected set; }
        public uint Ymax { get; protected set; }

        public Blocks(uint Xm, uint Ym)
        {
            Xmax = Xm;
            Ymax = Ym;
            blocks = new IBlock<T>[Xm, Ym];
        }

        public IBlock<T> this[uint x,uint y]
        {
            get
            {
                return blocks[x,y];
            }
            set
            {
                blocks[x,y] = value;
                blocks[x, y].X = x;
                blocks[x, y].Y = y;
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
        }

        public void Click(IBlock<T> block)
        {
            Check(block);
            Click(block.X, block.Y);
        }

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

        public void Clear(bool value = false)
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
    }
}
