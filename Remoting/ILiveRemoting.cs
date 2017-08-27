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
                this.tcpClient.NewConnection += new NewConnectionEventHandler(tcpClient_NewConnection);
                this.tcpClient.Disconnected += new DisconnectedEventHandler(tcpClient_Disconnected);
                this.tcpClient.NetDataReceived += new DataReceivedEventHandler(tcpClient_DataReceived);
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

        void tcpClient_DataReceived(object sender, string message, EventArgs e)
        {
            ILiveDebug.Instance.WriteLine("RemotingData:" + message);
        }

        private bool HertEnable = false;
        void tcpClient_Disconnected(object sender, EventArgs e)
        {
            ILiveDebug.Instance.WriteLine("TCPClient:SOCKET_STATUS_NO_CONNECT Wait 60s");

            this.HertEnable = false;
            Thread.Sleep(60000);
            this.tcpClient.Start();
        }

        void tcpClient_NewConnection(EventArgs e)
        {
            this.HertEnable = true;
            new Thread(this.SendHeart,this,Crestron.SimplSharpPro.CrestronThread.Thread.eThreadStartOptions.Running);
        }


        public object SendHeart(object o)
        {
            while (this.HertEnable)
            {
               
                this.Send(Newtonsoft.Json.JsonConvert.SerializeObject(client));
                Thread.Sleep(60000);
                
            }
            return o;
        }
        public void Send(string p)
        {
            this.tcpClient.Send(p);
        }

    }
}