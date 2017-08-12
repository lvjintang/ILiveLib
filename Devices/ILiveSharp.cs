using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using ILiveLib;

namespace ILiveLib
{
    /*
     波 特 率：9600 数 据 位：8 停 止 位：1 奇偶校验：0 
504F5752202020310D   OPEN
504F5752202020300D   Close
52435352202020320D   HDMI1
52445352202020320D   HDMI2
52425356202020320D   VIDEO
     */

    /// <summary>
    /// 夏普投影仪
    /// </summary>
    public class ILiveSharp
    {
                INetPortDevice port = null;
                public ILiveSharp(INetPortDevice port)
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
            byte[] sendBytes = new byte[] { 0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x31, 0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes,0,sendBytes.Length);
            port.Send(data);
        }
        public void PowerOff()
        {
            //504F5752202020300D
            byte[] sendBytes = new byte[] { 0x50, 0x4F, 0x57, 0x52, 0x20, 0x20, 0x20, 0x30, 0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            port.Send(data);
        }
        public void HDMI1()
        {
            byte[] sendBytes = new byte[] { 0x52,0x43,0x53,0x52,0x20,0x20,0x20,0x32,0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            port.Send(data);
        }
        public void HDMI2()
        {
            byte[] sendBytes = new byte[] { 0x52,0x44,0x53,0x52,0x20,0x20,0x20,0x32,0x0D };
            string data = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            port.Send(data);
        }
    }
}