using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elfin.IO.Common;
using Elfin.IO.Files;

namespace Elfin.IO.CSV
{
    /// <summary>
    /// CSV 文件辅助类
    /// </summary>
    public class CSVHelper
    {
        /// <summary>
        /// 读取指定路径的CSV文件
        /// </summary>
        /// <param name="path"></param>
        public void ReadCSVFile(string path)
        {
            var commentsInfoList = new List<string>();
            var csvFieldList = new List<string>();
            //// 按照行读取指定路径下的 CSV 文件
            var lineList = FileIOHelper.ReadFile(path);

            csvFieldList = GetCSVFieldList(lineList);
        }

        #region Private Functions

        /// <summary>
        /// 获取 CSV 文件字段集合
        /// </summary>
        /// <param name="lineList">行集合</param>
        /// <returns>CSV文件字段集合</returns>
        private List<string> GetCSVFieldList(List<string> lineList)
        {
            var commentsInfoList = new List<string>();
            var csvFieldList = new List<string>();

            //// 寻找文件字段
            foreach (var line in lineList)
            {
                var isSC = StringHelper.IsSingleLineComments(line);

                if (isSC)
                {
                    commentsInfoList.Add(line);
                }
                else
                {
                    var lineIndex = lineList.IndexOf(line);
                    var next1Line = lineList[lineIndex + 1];
                    var next2Line = lineList[lineIndex + 2];
                    var splitLineCount = line.Split(",").Count();
                    var splitNext1LineCount = line.Split(",").Count();
                    var splitNext2LineCount = line.Split(",").Count();

                    //// 当某一行与后两行通过","分割后的节点数量一致，则认定改行为字段名行
                    if (splitLineCount == splitNext1LineCount && splitLineCount == splitNext2LineCount)
                    {
                        csvFieldList = line.Split(",").ToList();
                        break;
                    }
                }
            }

            return csvFieldList;
        }

        

        #endregion
    }
}
