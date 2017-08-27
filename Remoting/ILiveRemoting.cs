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
        ILiveTCPClient tcpClient = null;
        private ClientInfo client = null;
        public ILiveRemoting(ClientInfo info)
        {
            this.client = info;
            this.InitConn();

        }
        /// <summary>
        /// 注册所有模块设备
        /// </summary>
        private void InitConn()
        {

            try
            {
                this.tcpClient = new ILiveTCPClient("smart.jtang.cc", 6000, 4096);
                this.tcpClient.ConnectedEvent += new NewConnectionEventHandler(tcpClient_ConnectedEvent);
                this.tcpClient.NetDataReceived += new NetDataReceivedEventHandler(tcpClient_NetDataReceived);
                this.tcpClient.Start();
                //初始化Socket
               // tcp = new TCPClient("smart.jtang.cc",6000,4096);
           
              //  tcp.SocketStatusChange += new TCPClientSocketStatusChangeEventHandler(tcp_SocketStatusChange);
                //this.Conn();
            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in Remoting: {0}", e.Message);
            }
        }

        void tcpClient_NetDataReceived(INetPortDevice device, NetPortSerialDataEventArgs args)
        {
            ILiveDebug.Instance.WriteLine("RemotingData:" + args.SerialData);
            //throw new NotImplementedException();
        }

        void tcpClient_ConnectedEvent(EventArgs e)
        {
            this.Send(Newtonsoft.Json.JsonConvert.SerializeObject(client));
           // ILiveDebug.Instance.WriteLine("RemotingConnected");
        }
        //private void Conn()
        //{
        //    if (this.tcp!=null)
        //    {
        //        //连接到服务器
        //        SocketErrorCodes codes= tcp.ConnectToServerAsync(this.TCPClientConnectCallback);
        //        ILiveDebug.Instance.WriteLine("ILiveRemoting:Conn:" + codes.ToString());
        //    }
        //}
        //void tcp_SocketStatusChange(TCPClient myTCPClient, SocketStatus clientSocketStatus)
        //{
        //  //  ILiveDebug.Instance.WriteLine("ILiveRemoting:tcp_SocketStatusChange:" + clientSocketStatus.ToString());
        //    if (clientSocketStatus==SocketStatus.SOCKET_STATUS_NO_CONNECT)
        //    {
        //        Thread.Sleep(60000);
        //        this.Conn();
        //    }
        //   // throw new NotImplementedException();
        //}
        ///// <summary>
        ///// 连接到服务器成功后回调函数
        ///// </summary>
        ///// <param name="myTCPClient"></param>
        //public void TCPClientConnectCallback(TCPClient myTCPClient)
        //{
        //    if (myTCPClient.ClientStatus==SocketStatus.SOCKET_STATUS_NO_CONNECT)
        //    {
        //        Thread.Sleep(60000);
        //        this.Conn();
        //    }
        //    if (myTCPClient.ClientStatus == SocketStatus.SOCKET_STATUS_CONNECTED)
        //    {
        //        Thread.Sleep(1000);
        //        if (this.ConnectedEvent!=null)
        //        {
        //            this.ConnectedEvent(new EventArgs());
                   
        //        }
        //    }
        //  //  ILiveDebug.Instance.WriteLine("ILiveRemoting:TCPClientConnectCallback:"+myTCPClient.ClientStatus);

        //    myTCPClient.ReceiveDataAsync(this.DataReceive);
        //}
        ///// <summary>
        ///// 数据接收回调函数
        ///// </summary>
        ///// <param name="myTCPClient"></param>
        ///// <param name="numberOfBytesReceived"></param>
        //void DataReceive(TCPClient myTCPClient, int numberOfBytesReceived)
        //{
        //   // ILiveDebug.Instance.WriteLine("ILiveRemoting:DataReceive");


        //    string messageReceived = string.Empty;
        //    byte[] readBuffer = new byte[numberOfBytesReceived];
        //    Array.Copy(myTCPClient.IncomingDataBuffer, readBuffer, numberOfBytesReceived);

        //    if (DataReceived != null)
        //    {
        //        DataReceived(this, readBuffer, EventArgs.Empty);
        //    }

        //    myTCPClient.ReceiveDataAsync(this.DataReceive);

        //}


        public void Send(string p)
        {
            this.tcpClient.Send(p);
        }

    }
}