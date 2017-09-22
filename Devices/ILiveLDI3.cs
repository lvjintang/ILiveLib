using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;

namespace ILiveLib
{
    /// <summary>
    /// 朗大I3  波特率9600
    /// </summary>
    public class ILiveLDI3
    {
        private INetPortDevice port = null;
       // public ComPort comMusicI3;
        public ILiveLDI3(INetPortDevice com)
        {
            this.port = com;
        }
        public void SendCMD(string cmd)
        {
            //数据头（0xFA） 房间号 指令码 组ID（0x00） 效验码 结束符（0xFE）
            switch (cmd)
            {
                case "PowerOn":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x51, 0x00, 0x4C, 0xFE });
                    break;
                case "PowerOff":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x52, 0x00, 0x4D, 0xFE });
                    break;
                case "SourceTF":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x70, 0x00, 0x6B, 0xFE });
                    break;
                case "SourceU":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x71, 0x00, 0x6C, 0xFE });

                    break;
                case "SourceRadio":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x76, 0x00, 0x71, 0xFE });

                    break;
                case "SourceAux":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x77, 0x00, 0x72, 0xFE });

                    break;
                case "SourceBlue":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x78, 0x00, 0x73, 0xFE });

                    break;
                case "Mute":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x43, 0x00, 0x3E, 0xFE });

                    break;
                case "MuteOff":
                    // this.SendData(new byte[] { 0xFA, 0x01, 0x52, 0x00, 0x71, 0x4D, 0xFE });
                    break;
                case "VolAdd":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x41, 0x00, 0x3C, 0xFE });
                    break;
                case "VolSub":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x42, 0x00, 0x3D, 0xFE });
                    break;
                case "Pause":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x38, 0x00, 0x33, 0xFE });
                    break;
                case "Play":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x37, 0x00, 0x32, 0xFE });
                    break;
                case "Prev":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x31, 0x00, 0x2C, 0xFE });
                    break;
                case "Next":
                    this.SendData(new byte[] { 0xFA, 0x01, 0x32, 0x00, 0x2D, 0xFE });
                    break;
                default:
                    break;
            }
        }
        public void SendCMD(int zone,string cmd)
        {
            switch (cmd)
            {
                case "PowerOn":
                    this.SendCmd((byte)zone, 0x51);
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x51, 0x00, 0x4C, 0xFE });
                    break;
                case "PowerOff":
                    this.SendCmd((byte)zone, 0x52);
                   // this.SendData(new byte[] { 0xFA, 0x01, 0x52, 0x00, 0x4D, 0xFE });
                    break;
                case "SourceTF":
                    this.SendCmd((byte)zone, 0x70);
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x70, 0x00, 0x6B, 0xFE });
                    break;
                case "SourceU":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x71, 0x00, 0x6C, 0xFE });
                    this.SendCmd((byte)zone, 0x71);
                    break;
                case "SourceRadio":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x76, 0x00, 0x71, 0xFE });
                    this.SendCmd((byte)zone, 0x76);
                    break;
                case "SourceAux":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x77, 0x00, 0x72, 0xFE });
                    this.SendCmd((byte)zone, 0x77);
                    break;
                case "SourceBlue":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x78, 0x00, 0x73, 0xFE });
                    this.SendCmd((byte)zone, 0x78);
                    break;
                case "Mute":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x43, 0x00, 0x3E, 0xFE });
                    this.SendCmd((byte)zone, 0x43);

                    break;
                case "MuteOff":
                    // this.SendData(new byte[] { 0xFA, 0x01, 0x52, 0x00, 0x71, 0x4D, 0xFE });
                    break;
                case "VolAdd":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x41, 0x00, 0x3C, 0xFE });
                    this.SendCmd((byte)zone, 0x41);

                    break;
                case "VolSub":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x42, 0x00, 0x3D, 0xFE });
                    this.SendCmd((byte)zone, 0x42);

                    break;
                case "Pause":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x38, 0x00, 0x33, 0xFE });
                    this.SendCmd((byte)zone, 0x38);

                    break;
                case "Play":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x37, 0x00, 0x32, 0xFE });
                    this.SendCmd((byte)zone, 0x37);

                    break;
                case "Prev":
                  //  this.SendData(new byte[] { 0xFA, 0x01, 0x31, 0x00, 0x2C, 0xFE });
                    this.SendCmd((byte)zone, 0x31);

                    break;
                case "Next":
                    //this.SendData(new byte[] { 0xFA, 0x01, 0x32, 0x00, 0x2D, 0xFE });
                    this.SendCmd((byte)zone, 0x32);

                    break;
                default:
                    break;
            }
        }
        private void SendCmd(byte zone, byte cmd)
        {
            byte[] data = new byte[] { 0xFA, zone, cmd, 0x00, 0x00, 0xFE };

            for (int i = 0; i < 4; i++)
            {
                data[4] += data[i];
            }
            this.SendData(data);
        }

        private void SendData(byte[] sendbytes)
        {
            ILiveDebug.Instance.WriteLine("I3ComSendData" + ILiveUtil.ToHexString(sendbytes));
          //  string cmd = Encoding.GetEncoding(28591).GetString(sendbytes, 0, sendbytes.Length);

            this.port.Send(sendbytes);
            Thread.Sleep(300);
        }
    
    }
}