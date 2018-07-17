using System;
using System.Collections.Generic;
using System.Text;

namespace Elfin.Toolkits.Tools
{
    public class SplitListHelper
    {
        /// <summary>
        /// 按指定长度分割 List
        /// </summary>
        /// <param name="Length">指定分割长度</param>
        /// <returns></returns>
        public static List<List<T>> SplitListByLength<T>(int Length, List<T> list)
        {
            var result = new List<List<T>>();
            //// 计数索引
            var index = 0;
            //// list总长度
            var listTotalCount = list.Count;
            //// 按照分割长度所要分割份数
            var splitNumber = listTotalCount / Length;
            //// 是否有余数(是否整除)
            var hasRemainder = false;

            if ((listTotalCount % Length) != 0)
            {
                //// 有余数,即：没有整除
                hasRemainder = true;
            }

            //// 对整除部分进行处理
            for (int i = 0; i < splitNumber; i++)
            {
                var nodeList = new List<T>();
                //// 开始索引
                var startIndex = index;
                //// 循环次数
                var cycles = Length + index;

                for (int n = startIndex; n < cycles; n++)
                {
                    nodeList.Add(list[n]);
                    index++;
                }

                result.Add(nodeList);
            }

            //// 对余数部分进行处理
            if (hasRemainder)
            {
                var remainder = listTotalCount % Length;
                //// 开始索引(注意index的值)
                var startIndex = index;
                //// 循环次数
                var cycles = remainder + index;
                var nodeList = new List<T>();

                for (var i = startIndex; i < cycles; i++)
                {
                    nodeList.Add(list[i]);
                }

                result.Add(nodeList);
            }

            return result;
        }
    }
}
