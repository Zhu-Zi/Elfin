using System;
using System.Collections.Generic;
using System.Text;

namespace Elfin.IO.Common
{
    /// <summary>
    /// 字符串操作辅助类
    /// </summary>
    public class StringHelper
    {
        /// <summary>
        /// 是否为单行
        /// 即:是否满足 C#,Python 的单行注释语法
        /// </summary>
        /// <param name="lineStr">行字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSingleLineComments(string lineStr)
        {
            if (string.IsNullOrEmpty(lineStr))
            {
                var firstChar = lineStr.Substring(0);

                //// 判断是否符合 Python 单行注释语法
                if (firstChar == "#")
                {
                    return true;
                }
                else
                {
                    var secoundChar = lineStr.Substring(1);

                    //// 判断是否符合 C# 单行注释语法
                    if (firstChar == "/" && secoundChar == "/")
                    {
                        return true;
                    }
                } 
            }

            return false;
        }
    }
}
