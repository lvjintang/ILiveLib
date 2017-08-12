using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronSockets;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib;

namespace ILiveLib
{
    /// <summary>
    /// 丽讯投影仪  波特率：9600
    /// </summary>
    public class ILiveVivitek
    {
        INetPortDevice server = null;
        public ILiveVivitek(INetPortDevice port)
        {
            try
            {
                this.server = port;
            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }
        public void PowerOn()
        {
            byte[] sendBytes = new byte[] {0x56,0x39,0x39,0x53,0x30,0x30,0x30,0x31,0x0D  };
            string data= Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
        public void PowerOff()
        {
            byte[] sendBytes = new byte[] { 0x56, 0x39, 0x39, 0x53, 0x30, 0x30, 0x30, 0x32, 0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
        public void HDMI1()
        {
            byte[] sendBytes = new byte[] { 0x56, 0x39, 0x39, 0x53, 0x30, 0x32, 0x30, 0x36, 0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
        private void HDMI2()
        {
            byte[] sendBytes = new byte[] { 0x56, 0x39, 0x39, 0x53, 0x30, 0x32, 0x30, 0x39, 0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

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