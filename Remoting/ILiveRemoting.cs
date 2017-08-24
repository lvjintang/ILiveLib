using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronSockets;
using Crestron.SimplSharpPro.CrestronThread;

namespace ILiveLib.Remoting
{
    public class ILiveRemoting
    {
        /// <summary>
        /// 数据接收委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public delegate void BytesReceivedEventHandler(Object sender, byte[] data, EventArgs e);

        public event BytesReceivedEventHandler DataReceived;

        TCPClient tcp = null;
        public ILiveRemoting()
        {
            this.InitConn();

        }
        /// <summary>
        /// 注册所有模块设备
        /// </summary>
        private void InitConn()
        {

            try
            {
                //初始化Socket
                tcp = new TCPClient("smart.jtang.cc",6000,4096);
           
                tcp.SocketStatusChange += new TCPClientSocketStatusChangeEventHandler(tcp_SocketStatusChange);
                //this.Conn();
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in Remoting: {0}", e.Message);
            }
        }
        private void Conn()
        {
            if (this.tcp!=null)
            {
                //连接到服务器
                SocketErrorCodes codes= tcp.ConnectToServerAsync(this.TCPClientConnectCallback);
                ILiveDebug.Instance.WriteLine("ILiveRemoting:Conn:" + codes.ToString());
            }
        }
        void tcp_SocketStatusChange(TCPClient myTCPClient, SocketStatus clientSocketStatus)
        {
          //  ILiveDebug.Instance.WriteLine("ILiveRemoting:tcp_SocketStatusChange:" + clientSocketStatus.ToString());
            if (clientSocketStatus==SocketStatus.SOCKET_STATUS_NO_CONNECT)
            {
                Thread.Sleep(360000);
                this.Conn();
            }
           // throw new NotImplementedException();
        }
        /// <summary>
        /// 连接到服务器成功后回调函数
        /// </summary>
        /// <param name="myTCPClient"></param>
        public void TCPClientConnectCallback(TCPClient myTCPClient)
        {
            if (myTCPClient.ClientStatus==SocketStatus.SOCKET_STATUS_NO_CONNECT)
            {
                Thread.Sleep(360000);
                this.Conn();
            }
          //  ILiveDebug.Instance.WriteLine("ILiveRemoting:TCPClientConnectCallback:"+myTCPClient.ClientStatus);

            myTCPClient.ReceiveDataAsync(this.DataReceive);
        }
        /// <summary>
        /// 数据接收回调函数
        /// </summary>
        /// <param name="myTCPClient"></param>
        /// <param name="numberOfBytesReceived"></param>
        void DataReceive(TCPClient myTCPClient, int numberOfBytesReceived)
        {
           // ILiveDebug.Instance.WriteLine("ILiveRemoting:DataReceive");


            string messageReceived = string.Empty;
            byte[] readBuffer = new byte[numberOfBytesReceived];
            Array.Copy(myTCPClient.IncomingDataBuffer, readBuffer, numberOfBytesReceived);

            if (DataReceived != null)
            {
                DataReceived(this, readBuffer, EventArgs.Empty);
            }

            myTCPClient.ReceiveDataAsync(this.DataReceive);

        }


        public void Send(string p)
        {

            byte[] datas = Encoding.GetEncoding(28591).GetBytes(p);
            if (tcp!=null&&tcp.ClientStatus==SocketStatus.SOCKET_STATUS_CONNECTED)
            {
               // ILiveDebug.Instance.WriteLine("ILiveRemoting:Send");

                tcp.SendData(datas, datas.Length);

            }
        }

        public void Start()
        {
            this.Conn();
           // new Thread(new ThreadCallbackFunction(this.Conn), this, Thread.eThreadStartOptions.Running);
        }
    }
}