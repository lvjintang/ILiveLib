using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronSockets;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro;

namespace ILiveLib
{
    public class ILiveTCPClient : INetPortDevice
    {
        ///// <summary>
        ///// 数据接收委托
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="message"></param>
        ///// <param name="e"></param>
        //public delegate void BytesReceivedEventHandler(Object sender, byte[] data, EventArgs e);

        //public event BytesReceivedEventHandler DataReceived;
        public event DataReceivedEventHandler NetDataReceived;

        public event DisconnectedEventHandler Disconnected;

        public event NewConnectionEventHandler NewConnection;

        TCPClient tcp = null;
        public ILiveTCPClient(string addr,int remoteport,int localport)
        {
            this.InitConn(addr,remoteport,localport);

        }
        /// <summary>
        /// 注册所有模块设备
        /// </summary>
        private void InitConn(string addr, int remoteport, int localport)
        {

            try
            {
                //初始化Socket
                tcp = new TCPClient(addr, remoteport, localport);

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
            if (this.tcp != null)
            {
                //连接到服务器
                SocketErrorCodes codes = tcp.ConnectToServerAsync(this.TCPClientConnectCallback);
            }
        }
        void tcp_SocketStatusChange(TCPClient myTCPClient, SocketStatus clientSocketStatus)
        {
            //  ILiveDebug.Instance.WriteLine("ILiveRemoting:tcp_SocketStatusChange:" + clientSocketStatus.ToString());
            if (clientSocketStatus == SocketStatus.SOCKET_STATUS_LINK_LOST || clientSocketStatus==SocketStatus.SOCKET_STATUS_NO_CONNECT)
            {
                if (this.Disconnected!=null)
                {
                    this.Disconnected(this, null);
                }
            }
        }
        /// <summary>
        /// 连接到服务器成功后回调函数
        /// </summary>
        /// <param name="myTCPClient"></param>
        public void TCPClientConnectCallback(TCPClient myTCPClient)
        {
            if (myTCPClient.ClientStatus != SocketStatus.SOCKET_STATUS_CONNECTED)
            {
                //连接失败 重新连接
                ILiveDebug.Instance.WriteLine("TCPClient:SOCKET_STATUS_NO_CONNECT Wait 60s");
                Thread.Sleep(60000);
                this.Conn();
            }
            if (myTCPClient.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
            {
                Thread.Sleep(1000);
                if (this.NewConnection != null)
                {
                    ILiveDebug.Instance.WriteLine("TCPClient:" + myTCPClient.ClientStatus.ToString());

                    this.NewConnection(null);

                }

                myTCPClient.ReceiveDataAsync(this.DataReceive);

            }
            //  ILiveDebug.Instance.WriteLine("ILiveRemoting:TCPClientConnectCallback:"+myTCPClient.ClientStatus);

        }
        /// <summary>
        /// 数据接收回调函数
        /// </summary>
        /// <param name="myTCPClient"></param>
        /// <param name="numberOfBytesReceived"></param>
        void DataReceive(TCPClient myTCPClient, int numberOfBytesReceived)
        {
            if (numberOfBytesReceived > 0)
            {
                // string messageReceived = string.Empty;
                byte[] readBuffer = new byte[numberOfBytesReceived];
                Array.Copy(myTCPClient.IncomingDataBuffer, readBuffer, numberOfBytesReceived);

                string messageReceived = Encoding.GetEncoding(28591).GetString(readBuffer, 0, numberOfBytesReceived);
                if (this.NetDataReceived != null)
                {
                    NetDataReceived(this, messageReceived, null);

                }
            }
            if (myTCPClient.ClientStatus==SocketStatus.SOCKET_STATUS_CONNECTED)
            {
                myTCPClient.ReceiveDataAsync(this.DataReceive);

            }


        }




        public void Start()
        {
            this.Conn();
        }



   

        public void Send(string dataToTransmit)
        {
            byte[] datas = Encoding.GetEncoding(28591).GetBytes(dataToTransmit);

            this.Send(datas);
        }

        public void Send(byte[] data)
        {
            if (tcp != null && tcp.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
            {

                tcp.SendData(data, data.Length);
            }
        }


        #region INetPortDevice 成员

       
        #endregion
    }
}