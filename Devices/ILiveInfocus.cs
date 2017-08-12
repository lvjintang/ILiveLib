using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronSockets;
using Crestron.SimplSharpPro.CrestronThread;

namespace ILiveLib
{
    /// <summary>
    /// 富可视投影仪 波特率19200 已在8515测试成功
    /// 
    /// </summary>
    public class ILiveInfocus
    {
        public UDPServer server = new UDPServer();

        /// <summary>
        /// 接收事件
        /// </summary>
        private Thread tcpListenHandler;


        public delegate void Push16IHandler(int id,int btnid, bool iChanStatus);

        public event Push16IHandler Push16IEvent;


        public ILiveInfocus(string ip, int port)
        {
            try
            {

                server.EnableUDPServer(ip, 6004, port);
                SocketErrorCodes code = server.ReceiveDataAsync(this.Read);

            }
            catch (Exception)
            {
              //  CrestronConsole.PrintLine(ex.Message);
                // ILiveDebug.Instance.WriteLine(ex.Message);
            }


        }

        private void PowerOn()
        {
            byte[] sendBytes = new byte[] {0x28,0x50,0x57,0x52,0x31,0x29  };
            this.server.SendData(sendBytes, sendBytes.Length);

            Thread.Sleep(200);
        }
        private void PowerOff()
        {
            byte[] sendBytes = new byte[] { 0x28, 0x50, 0x57, 0x52, 0x30, 0x29 };
            this.server.SendData(sendBytes, sendBytes.Length);

            Thread.Sleep(200);
        }
        private void HDMI1()
        {
            byte[] sendBytes = new byte[] { 0x28, 0x50, 0x57, 0x43, 0x35, 0x29 };
            this.server.SendData(sendBytes, sendBytes.Length);

            Thread.Sleep(200);
        }
        private void HDMI2()
        {
            byte[] sendBytes = new byte[] { 0x28, 0x50, 0x57, 0x43, 0x36, 0x29 };
            this.server.SendData(sendBytes, sendBytes.Length);

            Thread.Sleep(200);
        }



        private void Read(UDPServer myUDPServer, int numberOfBytesReceived)
        {
            byte[] rbytes = new byte[numberOfBytesReceived];

            if (numberOfBytesReceived > 0)
            {

                //   Array.Copy(myUDPServer.IncomingDataBuffer, rbytes, numberOfBytesReceived);

                string messageReceived = Encoding.GetEncoding(28591).GetString(myUDPServer.IncomingDataBuffer, 0, numberOfBytesReceived);


                OnDataReceived(messageReceived);
                // CrestronConsole.PrintLine("messageReceived:" + messageReceived);

            }
            try
            {
                // CrestronConsole.PrintLine("Recv:" + ILiveUtil.ToHexString(rbytes));
                SocketErrorCodes code = myUDPServer.ReceiveDataAsync(this.Read);
                Thread.Sleep(300);
            }
            catch (Exception)
            {


            }


        }
        void OnDataReceived(string serialData)
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(serialData);

        }
    }
}