using System;
using System.Collections.Generic;
using System.Text;

namespace Elfin.Toolkits.Tools
{
    /// <summary>
    /// 字符串辅助类
    /// </summary>
    public class StringHelper
    {
        private static string[] _strs = new string[]
                                 {
                                  "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                  "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                                 };

        /// <summary>
        /// 创建指定长度的随机字符串
        /// </summary>
        /// <param name="count">字符串长度</param>
        /// <returns></returns>
        public static string CreateNonce_str(int count)
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = _strs.Length;
            for (int i = 0; i < count; i++)
            {
                sb.Append(_strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// HTML 字符串转义
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TransferHTML(string str)
        {
            if (str == null)
            {
                return "";
            }
        
            return str.Replace("&", "&amp;").Replace("<", "&lt;")
                        .Replace(">", "&gt;").Replace("\"", "&quot;");
        }

        /// <summary>
        /// HTML 转义字符串还原
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string AntiTranferHTML(string str)
        {
            if (str == null)
            {
                return "";
            }

            return str.Replace("&amp;", "&").Replace("&lt;", "<")
                        .Replace("&gt;", ">").Replace("&quot;", "\"");
        }
    }
}
