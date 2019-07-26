using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DCVLicenseServer.Web.Core.Utils
{
    public static class MD5Util
    {
        #region MD5 算法

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="input"> 待加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string MD5Encrypt(string input, Encoding encoding)
        {
            return HashAlg.HashEncrypt(MD5.Create(), input, encoding);
        }

        /// <summary>
        /// 验证 MD5 值
        /// </summary>
        /// <param name="input"> 未加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static bool VerifyMD5Value(string input, Encoding encoding)
        {
            return HashAlg.VerifyHashValue(MD5.Create(), input, MD5Encrypt(input, encoding), encoding);
        }
        #endregion MD5 算法


    }
}
