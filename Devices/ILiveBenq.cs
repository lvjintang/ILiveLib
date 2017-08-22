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
            byte[] sendBytes = new byte[] { 0x0D, 0x2A, 0x70, 0x6F, 0x77, 0x3D, 0x6F, 0x6E, 0x23, 0x0D };

           // byte[] sendBytes = new byte[] { 0x06, 0x14, 0x00, 0x03, 0x00, 0x34, 0x11, 0x00, 0x5C };
            ILiveDebug.Instance.WriteLine("BenqOn");
            //string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            port.Send(sendBytes);
        }
        public void PowerOff()
        {
            //5830303158
            //06140003003411015D
            //5830303258
            byte[] sendBytes = new byte[] { 0x0D,0x2A,0x70,0x6F,0x77,0x3D,0x6F,0x66,0x66,0x23,0x0D };
          //  byte[] sendBytes = new byte[] { 0x06, 0x14, 0x00, 0x04, 0x00, 0x34, 0x11, 0x01, 0x5E };

           // byte[] sendBytes = new byte[] { 0x06, 0x14, 0x00, 0x03, 0x00, 0x34, 0x11, 0x01, 0x5D };
            //ILiveDebug.Instance.WriteLine("BenqOff");
            port.Send(sendBytes);
        }
        public void HDMI1()
        { }
        public void HDMI2()
        { }
    }
}