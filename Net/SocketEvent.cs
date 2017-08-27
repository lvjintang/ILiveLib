using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    #region 状态 委托
    public delegate void LogEventHandler(string Msg);
    /// <summary>
    /// 服务器状态 未连接，等待连接，连接已建立
    /// </summary>
    public enum ServerStatusLevel { Off, WaitingConnection, ConnectionEstablished };
    /// <summary>
    /// 连接建立委托
    /// </summary>
    /// <param name="e"></param>
    public delegate void NewConnectionEventHandler(EventArgs e);
    /// <summary>
    /// 数据接收委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="message"></param>
    /// <param name="e"></param>
    public delegate void DataReceivedEventHandler(Object sender, string message, EventArgs e);
    /// <summary>
    /// 连接断开委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DisconnectedEventHandler(Object sender, EventArgs e);

    #endregion
}