using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    /// <summary>
    /// 镜面电视
    /// </summary>
    public class ILiveJingMian
    {
        private INetPortDevice port = null;
        public ILiveJingMian(INetPortDevice com)
        {
            this.port = com;
        }
        public void Up()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x17, 0xE8 };
            this.port.Send(data);
        }
        public void Down()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x0D, 0xF2 };
            this.port.Send(data);
        }
        public void Left()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x0C, 0xF3 };
            this.port.Send(data);
        }
        public void Right()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x05, 0xFA };
            this.port.Send(data);
        }
        public void Mute()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x14, 0xEB };
            this.port.Send(data);
        }
        public void VolUp()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x0A, 0xF5 };
            this.port.Send(data);
        }
        public void VolDown()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x40, 0xBF };
            this.port.Send(data);
        }

        public void Input()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x1B, 0xE4 };
            this.port.Send(data);
        }
        public void Menu()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x4E, 0xB1 };
            this.port.Send(data);
        }
        public void TV()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x57, 0xA8 };
            this.port.Send(data);
        }
        public void PC()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x4D, 0xB2 };
            this.port.Send(data);
        }
        public void HDMI()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x48, 0xB7 };
            this.port.Send(data);
        }
        public void OK()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x02, 0xFD };
            this.port.Send(data);
        }
        public void PowerOff()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x0B, 0xF4 };
            this.port.Send(data);
        }

        public void PUp()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x56, 0xA9 };
            this.port.Send(data);
        }
        public void PDown()
        {
            byte[] data = { 0xA0, 0xF0, 0x55, 0xFF, 0x5A, 0xA5 };
            this.port.Send(data);
        }
        /*

BTN62=AV|A0 F0 55 FF 47 B8|118,381
BTN64=多媒体|A0 F0 55 FF 13 EC|230,202
BTN63=静止|A0 F0 55 FF 59 A6|235,379
BTN66=AUTO|A0 F0 55 FF 16 E9|227,122
BTN73=S_VIDEO|A0 F0 55 FF 12 ED|175,290
BTN77=YPbPr|A0 F0 55 FF 4F B0|178,380
BTN78=EXIT|A0 F0 55 FF 01 FE|335,383
BTN79=1|A0 F0 55 FF 42 BD|119,473
BTN80=2|A0 F0 55 FF 43 BC|118,558
BTN81=3|A0 F0 55 FF 0F F0|117,642
BTN82=4|A0 F0 55 FF 1E E1|177,473
BTN83=5|A0 F0 55 FF 1D E2|177,558
BTN84=6|A0 F0 55 FF 1C E3|177,643
BTN85=7|A0 F0 55 FF 18 E7|237,476
BTN86=8|A0 F0 55 FF 45 BA|238,560
BTN87=9|A0 F0 55 FF 4C B3|236,643
BTN88=0|A0 F0 55 FF 56 A9|290,477
[COMM]
COMNAME=COM1
BAUDRATE=38400
BYTESIZE=8
PARITY=无
STOPBITS=1
[FRMPOSITION]
TOP=40
LEFT=55
WIDTH=803
HEIGHT=536
         */
    }
}