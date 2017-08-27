using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharpPro;

namespace ILiveLib
{

    public interface INetPortDevice
    {
        //
        // 摘要:
        //     Net data receive event handler
        event DataReceivedEventHandler NetDataReceived;

        // 摘要:
        //     Function to send a string out of the ComPort.
        //
        // 参数:
        //   dataToTransmit:
        //     Serial data to send out
        //
        // 异常:
        //   System.ArgumentNullException:
        //     The specified string to transmit is 'null'.
        void Send(string dataToTransmit);
        void Send(byte[] data);
    }
}