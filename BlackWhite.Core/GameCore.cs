using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    public class GameCore<T>
    {
        /// <summary>
        /// 游戏状态
        /// </summary>
        public enum States
        {
            /// <summary>
            /// 完成初始化
            /// </summary>
            Initialized,
            /// <summary>
            /// 完成随机生成
            /// </summary>
            Randomized,
            /// <summary>
            /// 已开始
            /// </summary>
            Started,
            /// <summary>
            /// 已结束(获胜)
            /// </summary>
            Ended,
            /// <summary>
            /// 已停止(失败)
            /// </summary>
            Stopped,
            /// <summary>
            /// 自由模式
            /// </summary>
            Free,
            /// <summary>
            /// 已关闭
            /// </summary>
            Closed
        }

        public States State { get; protected set; }

        protected Blocks<T> blocks1;

        public uint Count { get; protected set; }

        protected GameCore() { }

        public GameCore(Blocks<T> blocks, Random random)
        {
            Initialize(blocks);
            Randomize(random);
            Start();
        }

        protected void Initialize(Blocks<T> blocks)
        {
            blocks1 = blocks;
            State = States.Initialized;
        }

        protected void Randomize(Random random)
        {
            blocks1.GameRandomize(random);
            State = States.Randomized;
        }

        protected void Start()
        {
            Count = 0;
            State = States.Started;
        }

        public virtual bool Click(uint x, uint y)
        {
            blocks1.Click(x, y);
            if (blocks1.IsSame() && State == States.Started) 
            {
                State = States.Ended;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Click(IBlock<T> block)
        {
            blocks1.Check(block);
            return Click(block.X, block.Y);
        }

        public void Close()
        {
            blocks1 = null;
            State = States.Closed;
        }
    }
}
