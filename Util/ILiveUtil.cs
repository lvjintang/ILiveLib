using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace ILiveLib
{
    public class ILiveUtil
    {
        /// <summary>
        /// 添加一个字节到数组中
        /// </summary>
        /// <param name="source">原数据</param>
        /// <param name="to">需要添加的字节</param>
        /// <returns>添加后的字节</returns>
        public static byte[] AddByteToBytes(byte[] source, byte to)
        {
            List<byte> lTemp = new List<byte>();
            if (source != null)
            {
                lTemp.AddRange(source);
            }
            lTemp.Add(to);
            byte[] result = new byte[lTemp.Count];
            lTemp.CopyTo(result);
            return result;


        }
        /// <summary>
        /// 将byte数组转换为字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;

            if (bytes != null)
            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            } return hexString;

        }

    }
}