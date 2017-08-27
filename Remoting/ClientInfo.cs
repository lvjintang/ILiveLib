using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    public class ClientInfo
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Name { get;set; }
        /// <summary>
        /// 客户昵称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 系统唯一序列号
        /// </summary>
        public string SerialNumber { get; set; }
    }
}