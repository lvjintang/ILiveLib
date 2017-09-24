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
    /// 大华多功能模块
    /// RS485 9600
    /// </summary>
    public class ILiveDaHua
    {
        public delegate void Push16IHandler(int id, bool iChanStatus);
        private INetPortDevice port = null;
        public ILiveDaHua(INetPortDevice com)
        {
            #region 注册串口
            this.port = com;
            #endregion
        }

        /// <summary>
        /// 多功能控制器
        /// </summary>
        /// <param name="address">地址码</param>
        /// <param name="port">第几路 1-8</param>
        /// <param name="states">true：闭合 false：断开</param>
        public void Relay8SW8(int address, int port, bool states)
        {
            port -= 1;
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

            this.port.Send(sendBytes);
            Thread.Sleep(300);
        }

        /// <summary>
        /// 4路调光模块
        /// </summary>
        /// <param name="addr">地址码</param>
        /// <param name="port">第几路1-4</param>
        /// <param name="level">亮度0-8</param>
        public void DIM4(int addr, int port, int level)
        {
            port -= 1;
            byte cmd1 = 0x00;
            if (level==0)
            {
                cmd1=0x00;
            }
            else if (level==10)
            {
                cmd1=0x80;

            }
            else
            {
                cmd1=(byte)(0x80+level);
            }
            byte[] sendBytes = new byte[] { 0x55, 0x34, (byte)addr, (byte)(0x01 << port), 0x01, 0x01,cmd1 , 0x00 };
            int check = 0;
            foreach (var item in sendBytes)
            {
                check += Convert.ToInt32(item);
            }
            sendBytes[8] = (byte)check;

            this.port.Send(sendBytes);
            Thread.Sleep(300);
        }
    }
}