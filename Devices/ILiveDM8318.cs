using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;

namespace ILiveLib
{
    /// <summary>
    /// 奥斯迪8318
    /// </summary>
    public class ILiveDM8318
    {
        private INetPortDevice port = null;
        public ILiveDM8318(INetPortDevice com)
        {
            this.port = com;
        }

        void com_SerialDataReceived(ComPort ReceivingComPort, ComPortSerialDataEventArgs args)
        {
            byte[] sendBytes = Encoding.GetEncoding(28591).GetBytes(args.SerialData);
        }

        //F9 A5 55 地址 功能 长度 数据 校验

        //0x10 开关机 01开 00 关
        //设置分区 1 的节目源为 DVD 指令：F9 A5 55 01 11 01 41 54 
        /// <summary>
        /// 开关机
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="onoff"></param>
        public void MusicPower(int zone, bool onoff)
        {
            //byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x10, 0x01, 0x01, (byte)(zone + 0x10 + 0x01 + 0x01) };
            //this.Send(data);
            if (onoff)
            {
                byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x10, 0x01, 0x01 };
                this.Send(data);
            }
            else
            {
                byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x10, 0x01, 0x00 };
                this.Send(data);
            }

        }
        /// <summary>
        /// 设置音源
        /// </summary>
        /// <param name="zone">区域</param>
        /// <param name="souce">FM:0x11 TUNER:0x21 TV:0x31 DVD:041 AUX:0x51 PC iPOD MP3/USB:0x81 SD:0x91 BLUETOOH:0xA1 DLAN:0xB1 Internet radio:0xC1 </param>
        public void MusicSource(int zone, byte souce)
        {
            byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x11, 0x01, souce };
            this.Send(data);
        }
        /// <summary>
        /// 设置音量
        /// </summary>
        /// <param name="zone">区域</param>
        /// <param name="souce">音量（0-100）</param>
        public void VolSet(int zone, byte vol)
        {
            byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x12, 0x01, vol };
            this.Send(data);
        }
        /// <summary>
        /// 音量加减
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="change"></param>
        public void VolSet(int zone, bool change)
        {
            if (change)
            {
                byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x13, 0x01, 0x01 };
                this.Send(data);
            }
            else
            {
                byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x13, 0x01, 0x00 };
                this.Send(data);
            }

        }
        /// <summary>
        /// 播放模式设置
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="change">单曲播放：0x01 单曲循环：0x02 顺序播放:0x03 列表循环：0x04 随机播放：0x05</param>
        public void PlayModeSet(int zone, byte mode)
        {

            byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x15, 0x01, mode };
            this.Send(data);
        }
        /// <summary>
        /// 播放模式设置
        /// </summary>
        /// <param name="zone"></param>
        /// <param name="change">播放：0x01 暂停：0x02 停止:0x04 </param>
        ///  <param name="source"> </param>
        public void PlaySet(int zone, byte mode, byte source)
        {

            byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x19, 0x02, source, mode };
            this.Send(data);
        }
        /// <summary>
        /// 上一曲 下一曲
        /// </summary>
        /// <param name="zone">0x1A</param>
        /// <param name="mode">0x01:上一曲 0x10:下一曲</param>
        ///  <param name="source"> </param>
        public void MusicChangeSet(int zone, byte mode, byte source)
        {

            byte[] data = { 0xF9, 0xA5, 0x55, (byte)zone, 0x1A, 0x02, source, mode };
            this.Send(data);
        }
        private void Send(params byte[] data)
        {

            string senddata = this.GetCMDString(data);


            //byte[] sendBytes = Encoding.GetEncoding(28591).GetBytes(senddata);
            //ILiveDebug.Instance.WriteLine("MusicData:" + ILiveUtil.ToHexString(sendBytes));

            this.port.Send(senddata);
        }
        private string GetCMDString(params byte[] senddata)
        {
            byte[] crc = this.CheckCode(senddata);
            byte[] sendBytes = this.copybyte(senddata, crc);
            return Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
        }
        private byte[] copybyte(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }
        private byte[] CheckCode(byte[] source)
        {
            byte[] ret = new byte[1];
            ret[0] = 0x00;
            for (int i = 3; i < source.Length; i++)
            {
                ret[0] += source[i];
            }
            return ret;
        }
    }
}