using System;
using System.Collections.Generic;
using System.Text;

namespace Elfin.Toolkits.Tools
{
    /// <summary>
    /// 日期时间辅助类
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// 创建时间戳
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static long CreateTimestamp(DateTime datetime)
        {
            return (datetime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
