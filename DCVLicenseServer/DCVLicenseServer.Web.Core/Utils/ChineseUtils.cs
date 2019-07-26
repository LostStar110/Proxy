using System;
using System.Collections.Generic;
using System.Text;

namespace DCVLicenseServer.Web.Core.Utils
{
    public class ChineseUtils
    {
        /// <summary>
        /// 获取拼音
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPYString(string str)
        {
            string r = string.Empty;
            foreach (char obj in str)
            {
                try
                {
                    var t = TinyPinyin.Core.PinyinHelper.GetPinyin(obj);
                    r += t.Substring(0, 1);
                }
                catch
                {
                    r += obj.ToString();
                }
            }
            return r;
        }



    }
}
