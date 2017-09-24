using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;
using ILiveLib;

namespace ILiveLib
{
    public class ILiveIsin
    {
        private int addr = 0;
        INetPortDevice port = null;
        public ILiveIsin(INetPortDevice port)
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
        public ILiveIsin(int addr, INetPortDevice port)
            : this(port)
        {
            this.addr = addr;
        }

        #region 继电器
        public void RelayOpen()
        {
            this.Relay8SW8(addr, 255, 15, 1);
            Thread.Sleep(1000);
        }
        public void RelayClose()
        {
            this.Relay8SW8(addr, 255, 15, 0);
            Thread.Sleep(1000);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port1"></param>
        /// <param name="port2"></param>
        /// <param name="states">1开 2关 3置反</param>
        public void Relay8SW8(int port1, int port2, int states)
        {
            byte[] sendBytes = this.BuldCMDRelay(this.addr, port1, port2, states);
            this.SendCMD(sendBytes);
            // this.Relay8SW8(this.addr, port1, port2, states);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port1"></param>
        /// <param name="port2"></param>
        /// <param name="states">1开 0关 2置反</param>
        public void Relay8SW8(int address, int port1, int port2, int states)
        {
            byte[] sendBytes = this.BuldCMDRelay(address, port1, port2, states);
            this.SendCMD(sendBytes);
        }
        #endregion
        #region 调光
        #region 设置亮度
        public void SetDim(int addr, int port)
        {
            byte[] sendBytes = this.BuldCMDDim((byte)addr, 0xA3, (byte)port, 0x00, (byte)2);
            this.SendCMD(sendBytes);
        }
        /// <summary>
        /// 设置第一路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim1(int p)
        {

            byte[] sendBytes = this.BuldCMDDim((byte)addr, 0xA2, 0x01, 0x00, (byte)p);
            this.SendCMD(sendBytes);
        }
        /// <summary>
        /// 设置第二路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim2(int p)
        {

            byte[] sendBytes = this.BuldCMDDim((byte)addr, 0xA2, 0x02, 0x00, (byte)p);
            this.SendCMD(sendBytes);
        }
        /// <summary>
        /// 设置第三路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim3(int p)
        {
            byte[] sendBytes = this.BuldCMDDim((byte)addr, 0xA2, 0x04, 0x00, (byte)p);
            this.SendCMD(sendBytes);
        }
        /// <summary>
        /// 设置第四路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim4(int p)
        {
            byte[] sendBytes = this.BuldCMDDim((byte)addr, 0xA2, 0x08, 0x00, (byte)p);
            this.SendCMD(sendBytes);
        }
        #endregion
        #endregion
        #region 场景
        public void SetScence(int index)
        {
            byte[] sendBytes = this.BuildCMDScence(1, 167, index);
            this.SendCMD(sendBytes);
        }
        #endregion

        #region 生成指令
        /// <summary>
        /// 构建指令
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="a">通道A 8路</param>
        /// <param name="b">通道B 4路</param>
        /// <param name="fun">状态 开：1、关：0、置反：2</param>
        /// <returns></returns>
        private byte[] BuldCMDRelay(int id, int a, int b, int fun)
        {
            //byte[] checkarr1 = BitConverter.GetBytes(id + 161 + a + b + fun);

            byte[] sendBytes = new byte[] { 178, (byte)id, 161, (byte)a, (byte)b, (byte)fun, 0, 43 };
            for (int i = 1; i < 6; i++)
            {
                sendBytes[6] += sendBytes[i];
            }
            return sendBytes;
        }
        /// <summary>
        /// 构建调光指令
        /// </summary>
        /// <param name="id">地址</param>
        /// <param name="type">功能类型 开关：162 置反：163 调光+：163 调光-：163 设启亮点：164</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="fun">状态 开：255 关：0 其它：0-255  置反：2 亮度+：1 亮度-：0</param>
        /// <returns></returns>
        private byte[] BuldCMDDim(int id, int type, int a, int b, int fun)
        {
            //  byte[] checkarr1 = BitConverter.GetBytes(id + 161 + a + b + fun);

            byte[] sendBytes = new byte[] { 178, (byte)id, (byte)type, (byte)a, (byte)b, (byte)fun, 0, 43 };
            for (int i = 1; i < 6; i++)
            {
                sendBytes[6] += sendBytes[i];
            }
            return sendBytes;
        }
        /// <summary>
        /// 构建场景指令
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="tpye">类型 设置场景：165 保存场景 166 调用场景：167</param>
        /// <param name="index"></param>
        /// <returns></returns>
        private byte[] BuildCMDScence(int id, int type, int index)
        {
            byte[] sendBytes = new byte[] { 178, (byte)(id + 100), (byte)type, 0, 0, (byte)index, 0, 43 };
            for (int i = 1; i < 6; i++)
            {
                sendBytes[6] += sendBytes[i];
            }
            return sendBytes;
        }
        #endregion
        #region 发送指令
        private void SendCMD(byte[] cmd)
        {
            ILiveDebug.Instance.WriteLine("Relay:" + ILiveUtil.ToHexString(cmd));
            this.port.Send(cmd);
            Thread.Sleep(500);
        }
        #endregion

    }
    /// <summary>
    /// 爱联继电器
    /// </summary>
    public class ILiveIsinRelay
    {
        public ILiveIsinStatus Status = new ILiveIsinStatus();

        private int addr = 0;
        INetPortDevice port = null;
        public ILiveIsinRelay(INetPortDevice port)
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
        public ILiveIsinRelay(int addr, INetPortDevice port)
            : this(port)
        {
            this.addr = addr;

        }

        public void RelayOpen()
        {
            this.Status.Relay0 = true;
            this.Status.Relay1 = true;
            this.Status.Relay2 = true;
            this.Status.Relay3 = true;
            this.Status.Relay4 = true;
            this.Status.Relay5 = true;
            this.Status.Relay6 = true;
            this.Status.Relay7 = true;
            this.Status.Relay8 = true;
            this.Status.Relay9 = true;
            this.Status.Relay10 = true;
            this.Status.Relay11 = true;
            this.Relay8SW8(addr, 255, 15, 1);
            Thread.Sleep(1000);
            // this.Relay8SW8(4, 127, 15, false);
            // Thread.Sleep(1000);
            // this.Relay8SW8(5, 255, 15, false);
        }
        public void RelayClose()
        {
            this.Status.Relay0 = false;
            this.Status.Relay1 = false;
            this.Status.Relay2 = false;
            this.Status.Relay3 = false;
            this.Status.Relay4 = false;
            this.Status.Relay5 = false;
            this.Status.Relay6 = false;
            this.Status.Relay7 = false;
            this.Status.Relay8 = false;
            this.Status.Relay9 = false;
            this.Status.Relay10 = false;
            this.Status.Relay11 = false;

            this.Relay8SW8(addr, 255, 15, 0);
            Thread.Sleep(1000);
            // this.Relay8SW8(4, 127, 15, true);
            // Thread.Sleep(1000);
            //  this.Relay8SW8(5, 255, 15, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port1"></param>
        /// <param name="port2"></param>
        /// <param name="states">1开 2关 3置反</param>
        public void Relay8SW8(int port1, int port2, int states)
        {
            byte[] sendBytes = this.BuldCMDRelay(this.addr, port1, port2, states);
            this.SendCMD(sendBytes);
            // this.Relay8SW8(this.addr, port1, port2, states);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port1"></param>
        /// <param name="port2"></param>
        /// <param name="states">1开 2关 3置反</param>
        private void Relay8SW8(int address, int port1, int port2, int states)
        {
            byte[] sendBytes = this.BuldCMDRelay(address, port1, port2, 1);
            this.SendCMD(sendBytes);
        }

        private void SendCMD(byte[] cmd)
        {
            ILiveDebug.Instance.WriteLine("Relay:" + ILiveUtil.ToHexString(cmd));
            this.port.Send(cmd);
            Thread.Sleep(500);
        }
        /// <summary>
        /// 构建指令
        /// </summary>
        /// <param name="id">ID号</param>
        /// <param name="a">通道A 8路</param>
        /// <param name="b">通道B 4路</param>
        /// <param name="fun">状态 开：1、关：0、置反：2</param>
        /// <returns></returns>
        private byte[] BuldCMDRelay(int id, int a, int b, int fun)
        {
            //byte[] checkarr1 = BitConverter.GetBytes(id + 161 + a + b + fun);

            byte[] sendBytes = new byte[] { 178, (byte)id, 161, (byte)a, (byte)b, (byte)fun, (byte)fun, 0, 43 };
            for (int i = 1; i < 6; i++)
            {
                sendBytes[6] += sendBytes[i];
            }
            return sendBytes;
        }
        /// <summary>
        /// 构建调光指令
        /// </summary>
        /// <param name="id">地址</param>
        /// <param name="type">功能类型 开关：162 置反：163 调光+：163 调光-：163 设启亮点：164</param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="fun">状态 开：255 关：0 其它：0-255  置反：2 亮度+：1 亮度-：0</param>
        /// <returns></returns>
        private byte[] BuldCMDDim(int id, int type, int a, int b, int fun)
        {
            //  byte[] checkarr1 = BitConverter.GetBytes(id + 161 + a + b + fun);

            byte[] sendBytes = new byte[] { 178, (byte)id, (byte)type, (byte)a, (byte)b, (byte)fun, (byte)fun, 0, 43 };
            for (int i = 1; i < 6; i++)
            {
                sendBytes[6] += sendBytes[i];
            }
            return sendBytes;
        }
    }

    /// <summary>
    /// 爱联4路调光模块
    /// </summary>
    public class ILiveIsinDimmer
    {
        public ILiveIsinDimmerStatus Status = new ILiveIsinDimmerStatus();

        private int addr = 0;
        INetPortDevice port = null;
        public ILiveIsinDimmer(INetPortDevice port)
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
        public ILiveIsinDimmer(int addr, INetPortDevice port)
            : this(port)
        {
            this.addr = addr;

        }


        #region 设置亮度
        /// <summary>
        /// 设置第一路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim1(int p)
        {

            byte[] sendBytes = this.BuildCMD((byte)addr, 0xA2, 0x01, 0x00, (byte)p);
            ILiveDebug.Instance.WriteLine("DIM1" + ILiveUtil.ToHexString(sendBytes));
            string cmd = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);

            this.port.Send(cmd);
            this.Status.Dim1 = p;
            Thread.Sleep(500);
        }
        /// <summary>
        /// 设置第二路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim2(int p)
        {

            byte[] sendBytes = this.BuildCMD((byte)addr, 0xA2, 0x02, 0x00, (byte)p);
            ILiveDebug.Instance.WriteLine("DIM2" + ILiveUtil.ToHexString(sendBytes));
            string cmd = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);

            this.port.Send(cmd);
            this.Status.Dim2 = p;
            Thread.Sleep(500);
        }
        /// <summary>
        /// 设置第三路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim3(int p)
        {
            byte[] sendBytes = this.BuildCMD((byte)addr, 0xA2, 0x04, 0x00, (byte)p);
            string cmd = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);
            ILiveDebug.Instance.WriteLine("DIM3" + ILiveUtil.ToHexString(sendBytes));

            this.port.Send(cmd);
            this.Status.Dim3 = p;
            Thread.Sleep(500);
        }
        /// <summary>
        /// 设置第四路调光亮度
        /// </summary>
        /// <param name="p">亮度 0-255</param>
        public void SetDim4(int p)
        {
            byte[] sendBytes = this.BuildCMD((byte)addr, 0xA2, 0x08, 0x00, (byte)p);
            ILiveDebug.Instance.WriteLine("DIM4" + ILiveUtil.ToHexString(sendBytes));

            string cmd = Encoding.GetEncoding(28591).GetString(sendBytes, 0, sendBytes.Length);

            this.port.Send(cmd);
            this.Status.Dim4 = p;
            Thread.Sleep(500);
        }
        #endregion

        private byte[] BuildCMD(byte addr, byte fun, byte port1, byte port2, byte p)
        {
            /*
            * 1 B2 协议头 
            * 2 1-99 设备编号
            * 3 A2:开关 A3 置反+调光加减 A4：设定启亮点
            * 4 选中通道 
            *  00 选中 0 路 
            *  0F 选中 4 路 
            * 5 默认00
            * 6 00 亮度0 FF亮度100    00：调光- 01：调光+ 02：置反
            * 8 Check 校验和高位（2-7 位校验） 
            * 9 Check 校验和低位 10 2B 协议尾 
            */
            // byte[] checkarr1 = new byte[2];
            // this.ConvertIntToByteArray(address + port1 + port1 + 162, ref checkarr1);
            byte[] checkarr1 = BitConverter.GetBytes(addr + fun + port1 + port2 + p);

            byte[] sendBytes = new byte[] { 0xB2, addr, fun, port1, port2, p, checkarr1[0], 0x2B };
            return sendBytes;
        }

        /// <summary>
        /// 构建场景指令
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="tpye">类型 设置场景：165 保存场景 166 调用场景：167</param>
        /// <param name="index"></param>
        /// <returns></returns>
        private byte[] BuildCMDScence(int id, int type, int index)
        {
            byte[] sendBytes = new byte[] { 178, (byte)(id + 101), (byte)type, 0, 0, (byte)index, 0, 0, 43 };
            for (int i = 1; i < 6; i++)
            {
                sendBytes[6] += sendBytes[i];
            }
            return sendBytes;
        }
    }
    public class ILiveIsinDimmerStatus
    {
        public int Dim1 = 0;
        public int Dim2 = 0;
        public int Dim3 = 0;
        public int Dim4 = 0;
    }
    public class ILiveIsinStatus
    {
        public bool Relay0 = false;
        public bool Relay1 = false;
        public bool Relay2 = false;
        public bool Relay3 = false;
        public bool Relay4 = false;
        public bool Relay5 = false;
        public bool Relay6 = false;
        public bool Relay7 = false;
        public bool Relay8 = false;
        public bool Relay9 = false;
        public bool Relay10 = false;
        public bool Relay11 = false;
    }
}