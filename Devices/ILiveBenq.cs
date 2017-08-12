using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using ILiveLib;

namespace ILiveLib
{
    /// <summary>
    /// 明基投影仪
    /// 波特率115200
    /// </summary>
    public class ILiveBenq
    {
        INetPortDevice port = null;
        public ILiveBenq(INetPortDevice port)
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
            //5830303158
            byte[] sendBytes = new byte[] { 0x06, 0x14, 0x00, 0x03, 0x00, 0x34, 0x11, 0x00, 0x5C };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            port.Send(data);
        }
        public void PowerOff()
        {
            //5830303158
            //06140003003411015D
            //5830303258
            byte[] sendBytes = new byte[] { 0x06, 0x14, 0x00, 0x03, 0x00, 0x34, 0x11, 0x01, 0x5D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            port.Send(data);
        }
        public void HDMI1()
        { }
        public void HDMI2()
        { }
    }
}