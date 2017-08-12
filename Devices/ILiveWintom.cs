using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    /// <summary>
    /// 创明电动窗帘
    /// </summary>
    public class ILiveWintom
    {
        INetPortDevice port = null;
        public ILiveWintom(INetPortDevice p)
        {
            this.port = p;
        }

        public void WindowOpen()
        {
            this.Open(0);
        }
        public void WindowClose()
        {
            this.Close(0);

        }
        public void WindowStop()
        {
            this.Stop(0);

        }
        public void Window10()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0x20, 0x24 };
            this.port.Send(data);
        }
        public void Window20()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0x40, 0x44 };
            this.port.Send(data);
        }
        public void Window30()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0x60, 0x64 };
            this.port.Send(data);
        }
        public void Window40()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0x80, 0x84 };
            this.port.Send(data);
        }
        public void Window50()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0xA0, 0xA4 };
            this.port.Send(data);
        }
        public void Window60()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0xC0, 0xC4 };
            this.port.Send(data);
        }
        public void Window70()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0xE0, 0xE4 };
            this.port.Send(data);
        }
        public void LivingWindow100()
        {
            byte[] data = { 0x55, 0xAA, 0x04, 0x04, 0x00, 0xFF, 0x03 };
            this.port.Send(data);
        }
        public void Open(int addr)
        {
            this.SendCMD(addr, 0x03, 0x03);
        }
        public void Close(int addr)
        {
            this.SendCMD(addr, 0x03, 0x01);
        }
        public void Stop(int addr)
        {
            this.SendCMD(addr, 0x03, 0x02);
        }
        private void SendCMD(int addr, int fun, int pa)
        {
            ILiveDebug.Instance.WriteLine("Curtains:" + pa + ":" + addr);
            byte check = (byte)(pa + addr);
            byte[] data = { 0x55, 0xAA, (byte)fun, (byte)pa, 0x00, (byte)addr, check };
            ILiveDebug.Instance.WriteLine("Curtains:" + ILiveUtil.ToHexString(data));
            this.port.Send(data);
        }
    }
}