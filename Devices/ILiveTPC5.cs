﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    /// <summary>
    /// 施德朗5寸触摸屏
    /// </summary>
    public class ILiveTPC5
    {
        public delegate void PTCIHandler(int id, int btnid);

        public event PTCIHandler PushTPCIEvent;

        INetPortDevice iserver = null;
        public ILiveTPC5(INetPortDevice port)
        {
            try
            {
                this.iserver = port;
                this.iserver.NetDataReceived += new DataReceivedEventHandler(iserver_NetDataReceived);

            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }

        void iserver_NetDataReceived(object sender, string message, EventArgs e)
        {
            OnDataReceived(message);
        }

        List<byte> rdata = new List<byte>(6);

        void OnDataReceived(string serialData)
        {
            if (rdata.Count > 50)
            {
                rdata.Clear();
            }
            byte[] sendBytes = Encoding.ASCII.GetBytes(serialData);
            ILiveDebug.Instance.WriteLine("OnDataReceived:"+ILiveUtil.ToHexString(sendBytes));

            try
            {
                foreach (var item in sendBytes)
                {
                    if (item == 0x55)
                    {
                        rdata.Clear();
                    }
                    rdata.Add(item);
                    if (item == 0x0D && rdata.Count > 5)
                    {
                        this.ProcessData();
                    }
                }

            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }
        void ProcessData()
        {
            try
            {
                ILiveDebug.Instance.WriteLine(ILiveUtil.ToHexString(rdata.ToArray()));

                if (rdata.Count == 6 && rdata[0] == 0x55 && rdata[5] == 0x0D)
                {

                    byte iChanIdx = rdata[1];

                    int h = rdata[2];

                    int l = rdata[3];

                    // if (rdata[4] == 0x0D)//校验
                    {
                        if (this.PushTPCIEvent != null)
                        {
                            this.PushTPCIEvent(iChanIdx, (h * 256) + l);

                        }
                    }

                }

                rdata.Clear();

            }
            catch (Exception ex)
            {
                ILiveDebug.Instance.WriteLine(ex.Message);

            }
        }
    }
}