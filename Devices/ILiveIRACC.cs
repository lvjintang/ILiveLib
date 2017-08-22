using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;

namespace ILiveLib
{
    public enum IRACCFL
    {
        /// <summary>
        /// 特低风
        /// </summary>
        LL = 0x10,
        /// <summary>
        /// 低风
        /// </summary>
        L = 0x20,
        /// <summary>
        /// 中风
        /// </summary>
        M = 0x30,
        /// <summary>
        /// 高风
        /// </summary>
        H = 0x40,
        /// <summary>
        /// 特高风
        /// </summary>
        HH = 0x50
    }
    public enum IRACCMode
    {
        /// <summary>
        /// 通风
        /// </summary>
        TF = 0x00,
        /// <summary>
        /// 制热
        /// </summary>
        ZR = 0x01,
        /// <summary>
        /// 制冷
        /// </summary>
        ZL = 0x02,
        /// <summary>
        /// 自动
        /// </summary>
        ZD = 0x03,
        /// <summary>
        /// 温度点
        /// </summary>
        WD = 0x04,
        /// <summary>
        /// 除湿
        /// </summary>
        CS = 05
    }
    /// <summary>
    /// 空调网关IRACC
    /// </summary>
    public class ILiveIRACC
    {
        public delegate void Push16IHandler(int id, bool iChanStatus);
      /// <summary>
      /// 
      /// </summary>
 // public event Push16IHandler Push16IEvent;

                private INetPortDevice port = null;
                public ILiveIRACC(INetPortDevice com)
        {
            this.port = com;
        }

        //public ComPort comDaHua;
        //public ILiveIRACC(ComPort com)
        //{
        //    #region 注册串口
        //    comDaHua = com;
        //    comDaHua.SerialDataReceived += new ComPortDataReceivedEvent(comDaHua_SerialDataReceived);
        //    if (!comDaHua.Registered)
        //    {
        //        if (comDaHua.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
        //            ErrorLog.Error("COM Port couldn't be registered. Cause: {0}", comDaHua.DeviceRegistrationFailureReason);
        //        if (comDaHua.Registered)
        //            comDaHua.SetComPortSpec(ComPort.eComBaudRates.ComspecBaudRate9600,
        //                                                             ComPort.eComDataBits.ComspecDataBits8,
        //                                                             ComPort.eComParityType.ComspecParityNone,
        //                                                             ComPort.eComStopBits.ComspecStopBits1,
        //                                 ComPort.eComProtocolType.ComspecProtocolRS232,
        //                                 ComPort.eComHardwareHandshakeType.ComspecHardwareHandshakeNone,
        //                                 ComPort.eComSoftwareHandshakeType.ComspecSoftwareHandshakeNone,
        //                                 false);
        //    }
        //    #endregion
        //}
        public void SendIRACCPower(bool on, IRACCFL fl)
        {
            byte dd = 0x60;

            if (on)
            {
                dd = 0x61;
            }
            this.SendIRACC(0x01, 0x06, 0x07, 0xD0, (byte)fl, dd);//00
        }
        /// <summary>
        /// 发送中等风速开关
        /// </summary>
        /// <param name="group"></param>
        /// <param name="on"></param>
        public void SendIRACCPower(int group, bool on)
        {
            byte dd = 0x60;
            byte gp = (byte)(208 + (group * 3));
            if (on)
            {
                dd = 0x61;
            }
            this.SendIRACC(0x01, 0x06, 0x07, gp, 0x30, dd);//00            
        }
        //public void SendIRACCTest()
        //{
        //    byte[] data1 = {0x01,0x06,0x07,0xD3,0x30,0x61 };
        //    string senddata1 = this.GetCMDString(data1);
        //    this.comDaHua.Send(senddata1);
        //}
        public void SendIRACCPower(int group, bool on, IRACCFL fl)
        {

            byte dd = 0x60;
            byte gp = (byte)(208 + (group * 3));
            if (on)
            {
                dd = 0x61;
            }
            this.SendIRACC(0x01, 0x06, 0x07, gp, (byte)fl, dd);//00
        }
        /// <summary>
        /// 调节风量
        /// </summary>
        /// <param name="group"></param>
        /// <param name="on"></param>
        /// <param name="fl"></param>
        public void SendIRACCFL(int group, bool on, IRACCFL fl)
        {

            byte dd = 0x60;
            byte gp = (byte)(208 + (group * 3));
            if (on)
            {
                dd = 0x61;
            }
            this.SendIRACC(0x01, 0x06, 0x07, gp, (byte)fl, dd);//00
        }
        /// <summary>
        /// 风速
        /// </summary>
        /// <param name="group"></param>
        /// <param name="fl"></param>
        public void SendIRACCFL(int group, IRACCFL fl)
        {
            byte gp = (byte)(208 + (group * 3));
            this.SendIRACC(0x01, 0x06, 0x07, gp, (byte)fl, 0x61);//00
        }
        public void SendIRACCSetMode(int group, IRACCMode mode)
        {
            byte gp = (byte)(208 + (group * 3) + 1);

            this.SendIRACC(0x01, 0x06, 0x07, gp, 0x00, (byte)mode);//00
        }
        public void SendIRACCTemp(int group, int wendu)
        {
            byte gp = (byte)(208 + (group * 3) + 2);

            byte[] shi = System.BitConverter.GetBytes(wendu * 10);

            this.SendIRACC(0x01, 0x06, 0x07, gp, shi[1], shi[0]);//16

        }

        private void SendIRACC(params byte[] data)
        {

            string senddata = this.GetCMDString(data);

            //byte[] sendBytes = Encoding.GetEncoding(28591).GetBytes(senddata);
            //ILiveDebug.Instance.WriteLine("IRACCData:" + ILiveUtil.ToHexString(sendBytes));


            this.port.Send(senddata);
        }
        private string GetCMDString(params byte[] senddata)
        {


            byte[] crc = this.Crc_16(senddata);

            byte[] sendBytes = this.copybyte(senddata, crc);
            return Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
        }
        private byte[] Crc_16(byte[] source)
        {
            byte[] ret = new byte[2];

            int CRC = 0xFFFF;//set all 1

            if (source.Length <= 0)
                CRC = 0;
            else
            {
                foreach (var item in source)
                {
                    CRC = CRC ^ (int)item;
                    for (int i = 0; i <= 7; i++)
                    {
                        if ((CRC & 1) != 0)
                            CRC = (CRC >> 1) ^ 0xA001;
                        else
                            CRC = CRC >> 1;    //
                    }
                }

                ret[1] = (byte)((CRC & 0xff00) >> 8);//高位置
                ret[0] = (byte)(CRC & 0x00ff);  //低位置
            }
            return ret;
        }
        private byte[] copybyte(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }
        void comDaHua_SerialDataReceived(ComPort ReceivingComPort, ComPortSerialDataEventArgs args)
        {
            //int exeid = 0;

            byte[] sendBytes = Encoding.ASCII.GetBytes(args.SerialData);
        }
    }
}