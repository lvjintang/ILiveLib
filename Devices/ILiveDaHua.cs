using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;

namespace ILiveLib.Devices
{
    /// <summary>
    /// 大华多功能模块
    /// RS485 9600
    /// </summary>
    public class ILiveDaHua
    {
        public delegate void Push16IHandler(int id, bool iChanStatus);
        public event Push16IHandler Push16IEvent;

        public ComPort comDaHua;
        public ILiveDaHua(ComPort com)
        {
            #region 注册串口
            comDaHua = com;
            comDaHua.SerialDataReceived += new ComPortDataReceivedEvent(comDaHua_SerialDataReceived);
            if (!comDaHua.Registered)
            {
                if (comDaHua.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    ErrorLog.Error("COM Port couldn't be registered. Cause: {0}", comDaHua.DeviceRegistrationFailureReason);
                if (comDaHua.Registered)
                    comDaHua.SetComPortSpec(ComPort.eComBaudRates.ComspecBaudRate9600,
                                                                     ComPort.eComDataBits.ComspecDataBits8,
                                                                     ComPort.eComParityType.ComspecParityNone,
                                                                     ComPort.eComStopBits.ComspecStopBits1,
                                         ComPort.eComProtocolType.ComspecProtocolRS485,
                                         ComPort.eComHardwareHandshakeType.ComspecHardwareHandshakeNone,
                                         ComPort.eComSoftwareHandshakeType.ComspecSoftwareHandshakeNone,
                                         false);
            }
            #endregion
        }
        void comDaHua_SerialDataReceived(ComPort ReceivingComPort, ComPortSerialDataEventArgs args)
        {
            //int exeid = 0;

            // byte[] sendBytes = Encoding.ASCII.GetBytes(args.SerialData);
            //  ILiveDebug.Instance.WriteLine("485Data:"+ILiveUtil.ToHexString(sendBytes));
        }

        /// <summary>
        /// 多功能控制器
        /// </summary>
        /// <param name="address">地址码</param>
        /// <param name="port">第几路 0-7</param>
        /// <param name="states">true：闭合 false：断开</param>
        public void Relay8SW8(int address, int port, bool states)
        {
            //  55 13 03 01 01 02 02 00 71
            byte p = (byte)(0x01 << port);
            //00000010 00000000  00000000 00000000 
            //00001000 00000000  00000100 00000000
            byte cmd1 = 0x00;
            byte cmd2 = 0x00;
            if (port < 4)
            {
                if (states)
                {
                    cmd1 = (byte)(0x01 << (port * 2 + 1));
                }
                else
                {
                    cmd1 = (byte)(0x01 << (port * 2));
                }
            }
            else
            {
                if (states)
                {
                    cmd2 = (byte)(0x01 << ((port - 4) * 2 + 1));

                }
                else
                {
                    cmd2 = (byte)(0x01 << ((port - 4) * 2));
                }

            }
            //0x01<<(port*2+1)  0x01<<port*2
            //0x01<<3
            byte[] sendBytes = new byte[] { 0x55, 0x13, (byte)address, (byte)(0x01 << port), 0x01, 0x02, cmd1, cmd2, 0x00 };
            int check = 0;
            foreach (var item in sendBytes)
            {
                check += Convert.ToInt32(item);
            }
            sendBytes[8] = (byte)check;

            // ILiveDebug.Instance.WriteLine("dahua" + ILiveUtil.ToHexString(sendBytes));

            string cmd = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);

            this.comDaHua.Send(cmd);
            Thread.Sleep(200);
        }
    }
}