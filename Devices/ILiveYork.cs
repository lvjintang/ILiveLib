using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    /// <summary>
    /// 约克水机温控 9600
    /// </summary>
    public class ILiveYork
    {
        private INetPortDevice port = null;
        public ILiveYork(INetPortDevice com)
        {
            this.port = com;
        }
        /// <summary>
        /// 开关机
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="on"></param>
        public void SendYorkPowerOn(int addr, bool on)
        {
            if (on)
            {
                this.SendYork(addr, 2, 1);
            }
            else
            {
                this.SendYork(addr, 2, 0);

            }

        }

        /// <summary>
        /// 设置模式
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="mode">1：制冷 2：制热 3：通风</param>
        public void SendYorkMode(int addr, int mode)
        {
            this.SendYork(addr, 3, mode);
        }

        /// <summary>
        /// 设置温度
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="wd"></param>
        public void SendYorkWenDu(int addr, double wd)
        {
            int wddata = (int)wd * 10;
            this.SendYork(addr, 4, wddata);
        }
        /// <summary>
        /// 地址 功能码 寄存器地址 数据
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="?"></param>
        public void SendYork(int addr,int jc,int dt)
        {
            //   arry[0] = (byte)(m & 0xFF);  
            //arry[1] = (byte)((m & 0xFF00) >> 8);  

            byte[] dr = { (byte)addr, 0x06,(byte)((jc & 0xFF00) >> 8), (byte)(jc & 0xFF), (byte)((dt & 0xFF00) >> 8), (byte)(dt & 0xFF) };
            this.SendYork(dr);
        }
        private void SendYork(params byte[] data)
        {
            byte[] senddata = this.GetCMDString(data);
            ILiveDebug.Instance.WriteLine("YORK:"+ILiveUtil.ToHexString(senddata));
            this.port.Send(senddata);
        }
        private byte[] GetCMDString(params byte[] senddata)
        {
            byte[] crc = this.Crc_16(senddata);
            byte[] sendBytes = this.copybyte(senddata, crc);
            return sendBytes;
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
    }
}