using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using ILiveLib;

namespace ILiveLib
{
    /// <summary>
    /// 主要用于转换快思聪COM口
    /// </summary>
    public class ILiveComPort:INetPortDevice
    {
        private ComPort com = null;
        /// <summary>
        /// COM口
        /// </summary>
        /// <param name="com"></param>
        public ILiveComPort(ComPort com)
        {
            this.com = com;
            this.com.SerialDataReceived += new ComPortDataReceivedEvent(com_SerialDataReceived);
        }

        public void Register()
        {
            this.Register(ComPort.eComProtocolType.ComspecProtocolRS232, ComPort.eComBaudRates.ComspecBaudRate9600);
            ////if (!this.com.Registered)
            ////{
            ////    if (this.com.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
            ////        ErrorLog.Error("COM Port couldn't be registered. Cause: {0}", this.com.DeviceRegistrationFailureReason);
            ////    if (this.com.Registered)
            ////        this.com.SetComPortSpec(ComPort.eComBaudRates.ComspecBaudRate9600,
            ////                                                         ComPort.eComDataBits.ComspecDataBits8,
            ////                                                         ComPort.eComParityType.ComspecParityNone,
            ////                                                         ComPort.eComStopBits.ComspecStopBits1,
            ////                             ComPort.eComProtocolType.ComspecProtocolRS232,
            ////                             ComPort.eComHardwareHandshakeType.ComspecHardwareHandshakeNone,
            ////                             ComPort.eComSoftwareHandshakeType.ComspecSoftwareHandshakeNone,
            ////                             false);
                
            ////    ILiveDebug.Instance.WriteLine("COM Reg Sucess");
            ////}
        }
        public void Register(ComPort.eComProtocolType comspecProtocolRS232, Crestron.SimplSharpPro.ComPort.eComBaudRates baudrates)
        {
            if (!this.com.Registered)
            {
                if (this.com.Register() != eDeviceRegistrationUnRegistrationResponse.Success)
                    ErrorLog.Error("COM Port couldn't be registered. Cause: {0}", this.com.DeviceRegistrationFailureReason);
                if (this.com.Registered)
                    this.com.SetComPortSpec(baudrates,
                                                                     ComPort.eComDataBits.ComspecDataBits8,
                                                                     ComPort.eComParityType.ComspecParityNone,
                                                                     ComPort.eComStopBits.ComspecStopBits1,
                                         comspecProtocolRS232,
                                         ComPort.eComHardwareHandshakeType.ComspecHardwareHandshakeNone,
                                         ComPort.eComSoftwareHandshakeType.ComspecSoftwareHandshakeNone,
                                         false);

                ILiveDebug.Instance.WriteLine("COM Reg Sucess" + comspecProtocolRS232.ToString() + baudrates.ToString());
            }
        }
        void com_SerialDataReceived(ComPort ReceivingComPort, ComPortSerialDataEventArgs args)
        {
          //  byte[] sendBytes = Encoding.ASCII.GetBytes(args.SerialData);
            // ILiveDebug.Instance.WriteLine("485Length:" + sendBytes.Length.ToString() + "data:" + ILiveUtil.ToHexString(sendBytes));
<<<<<<< HEAD
          //  ILiveDebug.Instance.WriteLine(ReceivingComPort.ID + "COMDebug:" + ILiveUtil.ToHexString(sendBytes) );
=======
            ILiveDebug.Instance.WriteLine(ReceivingComPort.ID + "COMDebug:" + ILiveUtil.ToHexString(sendBytes) );
>>>>>>> 189a754e5397068ad0977d1d33b7f301799f646d
            if (!String.IsNullOrEmpty(args.SerialData))
            {
                if (this.NetDataReceived != null)
                {
                    this.NetDataReceived(this, args.SerialData, null);
                }
            }
      
        }
        #region INetPortDevice 成员

        public event DataReceivedEventHandler NetDataReceived;

        public void Send(string dataToTransmit)
        {
            if (this.com!=null)
            {
                try
                {
              
                    this.com.Send(dataToTransmit);
                    ILiveDebug.Instance.WriteLine("COM" + this.com.ID + this.com.Registered+":" + dataToTransmit);

                }
                catch (Exception)
                {
                    
                }
            }
        }

        #endregion

        #region INetPortDevice 成员


        public void Send(byte[] data)
        {
            string s=Encoding.GetEncoding(28591).GetString(data,0,data.Length);
            this.Send(s);
        }

        #endregion
    }
}