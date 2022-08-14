using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    public class FreeCore<T> : GameCore<T>
        where T : IBlock<T>, new()
    {
        /// <summary>
        /// 点击后的处理方式
        /// </summary>
        public enum ClickModes
        {
            /// <summary>
            /// 常规处理
            /// </summary>
            Normal,
            /// <summary>
            /// 仅改变点击的方块
            /// </summary>
            Change
        }

        public ClickModes ClickMode { get; set; }

        public FreeCore(Blocks<T> blocks)
        {
            Initialize(blocks);
            Free();
        }

        protected void Free()
        {
            blocks1.Clear();
            State = States.Free;
        }

        /*
        public override bool Click(uint x, uint y)
        {
            if (ClickMode == ClickModes.Normal)
            {
                blocks1.Click(x, y);
            }
            else
            {
                blocks1.Change(x, y);
            }
            return false;
        }
        */

        /// <summary>
        /// 完全随机生成
        /// </summary>
        /// <param name="random">随机数生成器</param>
        public void FullRandomize(Random random)
        {
            blocks1.FullRandomize(random);
        }

        public void MixRandomize(Random random)
        {
            blocks1.MixRandomize(random);
        }

        public void GameRandomize(Random random)
        {
            blocks1.GameRandomize(random);
        }
    }
}
