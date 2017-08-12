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
    public class ILiveOppo
    {
        INetPortDevice server = null;
        public ILiveOppo(INetPortDevice port)
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


        public void PowerOn()
        {
            byte[] sendBytes = new byte[] {0x23,0x50,0x4F,0x4E,0x0D  };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
        public void PowerOff()
        {
            byte[] sendBytes = new byte[] { 0x23,0x50,0x4F,0x46, 0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
      
    
        public void Read(TCPClient myTCPClient, int numberOfBytesReceived)
        {
            if (myTCPClient.ClientStatus != SocketStatus.SOCKET_STATUS_CONNECTED)
            {

                return;
            }
            string messageReceived = string.Empty;

            try
            {

                messageReceived = Encoding.GetEncoding(28591).GetString(myTCPClient.IncomingDataBuffer, 0, numberOfBytesReceived);

                OnDataReceived(messageReceived);

                myTCPClient.ReceiveDataAsync(this.Read, 0);


            }
            catch (Exception)
            {

                // if (Disconnected != null)
                //   Disconnected(this, EventArgs.Empty);
            }
        }
        void OnDataReceived(string serialData)
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(serialData);

        }
    }
}