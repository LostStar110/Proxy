using System;
using System.Collections.Generic;
using System.Text;

namespace DCVLicenseServer.Web.Core.Utils
{
    public static class Base64Util
    {
        /// <summary>
        /// Base64编码加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncryptString(string source)
        {
            string encodeStr = string.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(source);

            try
            {
                encodeStr = Convert.ToBase64String(bytes);
            }
            catch (Exception)
            {
                encodeStr = source;
            }
            return encodeStr;
        }
        /// <summary>
        /// 从Base64编码解密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DecryptString(string source)
        {
            string decode = "";

            try
            {
                byte[] bytes = Convert.FromBase64String(source);
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = source;
            }
            return decode;
        }

    }
}
