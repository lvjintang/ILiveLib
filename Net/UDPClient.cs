using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using Crestron.SimplSharp.CrestronSockets;
using Crestron.SimplSharpPro.CrestronThread;
using Crestron.SimplSharpPro;

namespace ILiveLib
{
    /// <summary>
    /// UDP客户端
    /// </summary>
    public class UDPClient : INetPortDevice
    {
        public event DataReceivedEventHandler NetDataReceived;

        public UDPServer server = null;

        public string host = string.Empty;
        public int remoteport = 0;
        public int localport = 0;
        /// <summary>
        /// 初始化UDP客户端
        /// </summary>
        /// <param name="host">远程地址</param>
        /// <param name="localport">本地端口</param>
        /// <param name="remoteport">远程端口</param>
        public UDPClient(string host, int localport, int remoteport)
        {
            this.host = host;
            this.localport = localport;
            this.remoteport = remoteport;
            server = new UDPServer();
        }
        /// <summary>
        /// 连接
        /// </summary>
        public void Connect()
        {
            this.Connect(true);
        }
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="enableRec">是否接收反馈</param>
        public void Connect(bool enableRec)
        {
            try
            {
                server.EnableUDPServer(this.host, this.localport, this.remoteport);
                if (enableRec)
                {
                    SocketErrorCodes code = server.ReceiveDataAsync(this.Read);

                }
            }
            catch (Exception)
            {


            }
        }
        private void Read(UDPServer myUDPServer, int numberOfBytesReceived)
        {
            byte[] rbytes = new byte[numberOfBytesReceived];

            if (numberOfBytesReceived > 0)
            {
                string messageReceived = Encoding.GetEncoding(28591).GetString(myUDPServer.IncomingDataBuffer, 0, numberOfBytesReceived);
                if (this.NetDataReceived!=null)
                {
                    NetDataReceived(this, messageReceived, null);

                }
            }
            try
            {
                SocketErrorCodes code = myUDPServer.ReceiveDataAsync(this.Read);
                Thread.Sleep(300);
            }
            catch (Exception)
            {


            }
        }


        public void DisConnect()
        {
            server.DisableUDPServer();
        }


        #region INetPortDevice 成员


        public void Send(string data)
        {

            byte[] sendBytes = Encoding.GetEncoding(28591).GetBytes(data);
            this.Send(sendBytes);
        }
        public void Send(byte[] data)
        {

            server.SendData(data, data.Length);
           // ILiveDebug.Instance.WriteLine("UDP" + ILiveUtil.ToHexString(data));
            //else
            //{
            //    ILiveDebug.Instance.WriteLine("success");
            //}
          //  ILiveDebug.Instance.WriteLine(server.RemotePortNumber+server.PortNumber+server.LocalAddressOfServer+"UDPData:" + ILiveUtil.ToHexString(data));

        }

        #endregion
    }
}