using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    public class ILiveASK
    {
        INetPortDevice port = null;
        public ILiveASK(INetPortDevice port)
        {
            try
            {
                this.port = port;
            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }

        public void PowerOn()
        {
            byte[] sendBytes = new byte[] { 0x4D,0x21,0x20,0x50,0x44,0x4D,0x20,0x31,0x0D };
            //string data = Encoding.GetEncoding(28591).GetString(sendBytes,0,sendBytes.Length);
            port.Send(sendBytes);
        }
        public void PowerOff()
        {
            //504F5752202020300D
            byte[] sendBytes = new byte[] { 0x4D, 0x21, 0x20, 0x50, 0x44, 0x4D, 0x20, 0x30, 0x0D };
            port.Send(sendBytes);
        }
        public void HDMI1()
        {
            byte[] sendBytes = new byte[] { 0x4D, 0x21, 0x20, 0x49, 0x4E, 0x50, 0x20, 0x30, 0x0D };
            port.Send(sendBytes);
        }
        public void HDMI2()
        {
            byte[] sendBytes = new byte[] { 0x4D, 0x21, 0x20, 0x49, 0x4E, 0x50, 0x20, 0x31, 0x0D };
            port.Send(sendBytes);
        }
    }
}