using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro.CrestronThread;

namespace ILiveLib
{
    /// <summary>
    /// 智嵌物联16路继电器模块
    /// </summary>
    public class ILiveZQWL16
    {
        /*
        48 3a 01 52 00 00 00 00 00 00 00 00 d5 45 44  读输入
        48 3a 01 57 00 00 00 00 00 00 00 00 da 45 44  输出关闭
        48 3a 01 57 01 01 01 01 01 01 01 01 e2 45 44  输出打开0~7
        48 3a 01 57 10 10 10 10 10 10 10 10 5a 45 44  输出打开8~15
        48 3a 01 57 11 11 11 11 11 11 11 11 62 45 44  输出全部打开
        48 3a 01 53 00 00 00 00 00 00 00 00 d6 45 44  读输出
         */

          //public ILiveIsinStatus Status = new ILiveIsinStatus();

        private int addr = 0;
        INetPortDevice port = null;
        public ILiveZQWL16(INetPortDevice port)
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
        public ILiveZQWL16(int addr, INetPortDevice port):this(port)
        {
            this.addr = addr;

        }

        public void RelayOpen(int i)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            switch (i)
            {
                case 1:
                    data = new byte[] { 0x12, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 2:
                    data = new byte[] { 0x21, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 3:
                    data = new byte[] { 0x22, 0x12, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 4:
                    data = new byte[] { 0x22, 0x21, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 5:
                    data = new byte[] { 0x22, 0x22, 0x12, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 6:
                    data = new byte[] { 0x22, 0x22, 0x21, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 7:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x12, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 8:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x21, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 9:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x12, 0x22, 0x22, 0x22 };
                    break;
                case 10:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x21, 0x22, 0x22, 0x22 };
                    break;
                case 11:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x12, 0x22, 0x22 };
                    break;
                case 12:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x21, 0x22, 0x22 };
                    break;
                case 13:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x12, 0x22 };
                    break;
                case 14:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x21, 0x22 };
                    break;
                case 15:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x12 };
                    break;
                case 16:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x21 };
                    break;
                default:
                    break;
            }

            this.SendData(this.addr, data);

        }
        public void RelayClose(int i)
        {
            byte[] data = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            switch (i)
            {
                case 1:
                    data = new byte[] { 0x02, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 2:
                    data = new byte[] { 0x20, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 3:
                    data = new byte[] { 0x22, 0x02, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 4:
                    data = new byte[] { 0x22, 0x20, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 5:
                    data = new byte[] { 0x22, 0x22, 0x02, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 6:
                    data = new byte[] { 0x22, 0x22, 0x20, 0x22, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 7:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x02, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 8:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x20, 0x22, 0x22, 0x22, 0x22 };
                    break;
                case 9:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x02, 0x22, 0x22, 0x22 };
                    break;
                case 00:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x20, 0x22, 0x22, 0x22 };
                    break;
                case 11:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x02, 0x22, 0x22 };
                    break;
                case 12:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x20, 0x22, 0x22 };
                    break;
                case 13:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x02, 0x22 };
                    break;
                case 14:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x20, 0x22 };
                    break;
                case 15:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x02 };
                    break;
                case 16:
                    data = new byte[] { 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x22, 0x20 };
                    break;
                default:
                    break;
            }

            this.SendData(this.addr, data);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data">8字节数组 每字节代表2路 如0x10 第一路断开 第二路闭合。继电器只识别0和1，其他数据不做任何动作，可以将该路赋值微其它值</param>
        public void SetRelay( byte[] data)
        {
            this.SendData(this.addr, data);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data">8字节数组 每字节代表2路 如0x10 第一路断开 第二路闭合。继电器只识别0和1，其他数据不做任何动作，可以将该路赋值微其它值</param>
        public void SetRelay(int addr,byte[] data)
        {
            this.SendData(addr, data);
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data">每字节代表2路 如0x10 第一路断开 第二路闭合。继电器只识别0和1，其他数据不做任何动作，可以将该路赋值微其它值</param>
        private void SendData(int address, byte[] data)
        {
            if (data.Length!=8)
	{
                return;
	}
            byte[] checkarr1 = BitConverter.GetBytes(0x48 + 0x3a + address + 0x57 + data[0]+ data[1]+ data[2]+ data[3]+ data[4]+ data[5]+ data[6]+ data[7]);

            byte[] sendBytes=new byte[]{0x48,0x3a,(byte)address,0x57,data[0],data[1],data[2],data[3],data[4],data[5],data[6],data[7],checkarr1[0],0x45,0x44};

            ILiveDebug.Instance.WriteLine("Relay:" + ILiveUtil.ToHexString(sendBytes));
            string cmd = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);

            this.port.Send(cmd);
            Thread.Sleep(500);

        }
    }
}