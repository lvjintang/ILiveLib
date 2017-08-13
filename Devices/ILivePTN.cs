using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using ILiveLib;

namespace ILiveLib
{
    /// <summary>
    /// PTN矩阵 波特率9600
    /// </summary>
    public class ILivePTN
    {
        INetPortDevice port = null;
        public ILivePTN(INetPortDevice port)
        {
            try
            {
                this.port = port;
            }
            catch (Exception )
            {
                // ILiveDebug.Instance.WriteLine(ex.Message);
            }
        }
        public void Switch(int input, int output)
        {
            this.port.Send(input + "V" + output + ".");
        }
    }
}