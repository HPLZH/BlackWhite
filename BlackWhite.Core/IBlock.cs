using System;
using System.Collections.Generic;
using System.Text;

namespace BlackWhite.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">填写实现此接口的类的名称</typeparam>
    public interface IBlock<T>
        where T : IBlock<T>, new()
    {
        bool Value { get; set; }
        uint X { get; set; }
        uint Y { get; set; }

        Blocks<T> Parent { get; set; }
        event EventHandler<BlockClickedEventArgs> BlockClicked;
    }
}
