using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    public class GameCore<T>
        where T : IBlock<T>, new()
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
            blocks1.BlockClicked += (object sender, BlockClickedEventArgs e) => { Count++; BlockClicked(sender,e); Clicked(); };
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

        /// <summary>
        /// 此事件在Count增加后立即发生，先于状态改变
        /// </summary>
        public event EventHandler<BlockClickedEventArgs> BlockClicked = delegate { };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns>返回结果指示是否需要进行后续操作</returns>
        protected virtual bool Clicked()
        {
            if (blocks1.IsSame() && State == States.Started)
            {
                State = States.Ended;
                GameEnded(this, EventArgs.Empty);
                return false;
            }
            else
            {
                return true;
            }
        }

        public event EventHandler GameEnded;

        public void Close()
        {
            blocks1 = null;
            State = States.Closed;
        }
    }
}
