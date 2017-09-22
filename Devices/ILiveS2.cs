using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.CrestronThread;

namespace ILiveLib.Devices
{
    /// <summary>
    /// S2电源时序器
    /// </summary>
    public class ILiveS2
    {
        //public IROutputPort myPort;//S2电源时序器
        private INetPortDevice port = null;
        public ILiveS2(INetPortDevice com)
        {
            // this.myPort = sys.IROutputPorts[1];
            // this.myPort.SetIRSerialSpec(eIRSerialBaudRates.ComspecBaudRate19200, eIRSerialDataBits.ComspecDataBits8, eIRSerialParityType.ComspecParityNone, eIRSerialStopBits.ComspecStopBits1, Encoding.ASCII);
            this.port = com;
        }
        public void S2Open1()
        {
            S2Relay(1, true);
        }
        public void S2Close1()
        {
            S2Relay(1, false);
        }
        /// <param name="port">第几路 1-8</param>
        /// <param name="status">true:闭合 false：断开</param>
        public void S2Relay(int port, bool status)
        {
            if (status)
            {
                //ILiveDebug.Instance.WriteLine(string.Format("*001O{0}#", port));

                //this.myPort.SendSerialData(string.Format("*001O{0}#", port));//
                string data = string.Format("[PR001O{0}T0]", port);

                this.port.Send(data);
                Thread.Sleep(300);
            }
            else
            {
                string data = string.Format("[PR001C{0}T0]", port);
                this.port.Send(data);
                Thread.Sleep(300);
                // ILiveDebug.Instance.WriteLine(string.Format("*001C{0}#", port));
                // this.myPort.SendSerialData(string.Format("*001C{0}#",port));//电脑
            }
        }

    }
}