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
    /// 雅马哈功放 9600
    /// </summary>
    public class ILiveYamaha
    {
        INetPortDevice server = null;
        public ILiveYamaha(INetPortDevice port)
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

            //404D41494E3A5057523D4F6E0D0A  
            //404D41494E3A5057523D4F6E0D0A
            byte[] sendBytes = new byte[] {0x40,0x4D,0x41,0x49,0x4E,0x3A,0x50,0x57,0x52,0x3D,0x4F,0x6E,0x0D,0x0A  };
            ILiveDebug.Instance.WriteLine("SmallAVPowerOn");
           // string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(sendBytes);

            Thread.Sleep(200);
            this.server.Send(sendBytes);
        }
        public void PowerOff()
        {
            //404D41494E3A5057523D5374616E6462790D0A
            byte[] sendBytes = new byte[] {0x40,0x4D,0x41,0x49,0x4E,0x3A,0x50,0x57,0x52,0x3D,0x53,0x74,0x61,0x6E,0x64,0x62,0x79,0x0D,0x0A };
           // string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            ILiveDebug.Instance.WriteLine("SmallAVPowerOn");

            this.server.Send(sendBytes);

            Thread.Sleep(200);
        }
        public void VolUp()
        {
            //404D41494E3A564F4C3D55700D0A
            byte[] sendBytes = new byte[] { 0x40,0x4D,0x41,0x49,0x4E,0x3A,0x56,0x4F,0x4C,0x3D,0x55,0x70, 0x0D, 0x0A };
            //string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(sendBytes);

            Thread.Sleep(200);
        }
        public void VolDown()
        {
            // 404D41494E3A564F4C3D446F776E0D0A
            byte[] sendBytes = new byte[] { 0x40,0x4D,0x41,0x49,0x4E,0x3A,0x56,0x4F,0x4C,0x3D,0x44,0x6F,0x77,0x6E, 0x0D, 0x0A };
            //string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(sendBytes);

            Thread.Sleep(200);
        }
        public void MuteOn()
        {
            byte[] sendBytes = new byte[] { 0x40,0x4D,0x41,0x49,0x4E,0x3A,0x4D,0x55,0x54,0x45,0x3D,0x4F,0x6E, 0x0D, 0x0A };
            //string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(sendBytes);

            Thread.Sleep(200);
        }
        public void MuteOff()
        {
            byte[] sendBytes = new byte[] { 0x40,0x4D,0x41,0x49,0x4E,0x3A,0x4D,0x55,0x54,0x45,0x3D,0x4F,0x66,0x66, 0x0D, 0x0A };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
        public void HDMI1()
        {
            byte[] sendBytes = new byte[] { 0x40, 0x4D, 0x41, 0x49, 0x4E, 0x3A, 0x49, 0x4E, 0x50, 0x3D, 0x41, 0x56, 0x31, 0x0D, 0x0A };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
        public void HDMI2()
        {
            byte[] sendBytes = new byte[] { 0x40,0x4D,0x41,0x49,0x4E,0x3A,0x49,0x4E,0x50,0x3D,0x41,0x56,0x32,0x0D,0x0A };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);

            Thread.Sleep(200);
        }
        private void HDMI3()
        {
            byte[] sendBytes = new byte[] { 0x40, 0x4D, 0x41, 0x49, 0x4E, 0x3A, 0x49, 0x4E, 0x50, 0x3D, 0x41, 0x56, 0x33, 0x0D, 0x0A };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);
            Thread.Sleep(200);
        }
        private void HDMI4()
        {
            byte[] sendBytes = new byte[] { 0x40, 0x4D, 0x41, 0x49, 0x4E, 0x3A, 0x49, 0x4E, 0x50, 0x3D, 0x41, 0x56, 0x34, 0x0D, 0x0A };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);
            Thread.Sleep(200);
        }
        private void HDMI5()
        {
            byte[] sendBytes = new byte[] { 0x40, 0x4D, 0x41, 0x49, 0x4E, 0x3A, 0x49, 0x4E, 0x50, 0x3D, 0x41, 0x56, 0x35, 0x0D, 0x0A };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            this.server.Send(data);
            Thread.Sleep(200);
        }
        private void HDMI6()
        {
            byte[] sendBytes = new byte[] { 0x40, 0x4D, 0x41, 0x49, 0x4E, 0x3A, 0x49, 0x4E, 0x50, 0x3D, 0x41, 0x56, 0x36, 0x0D, 0x0A };
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