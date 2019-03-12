using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elfin.Core.Builders;
using Elfin.Core.Models;
using Elfin.IO.Common;
using Elfin.IO.Files;
using Elfin.IO.Models;

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
        public static List<object> ReadCSVFile(string path)
        {
            var resutl = new List<object>();
            var commentsInfoList = new List<string>();
            var csvFieldModel = new CSVFieldModel();
            //// 按照行读取指定路径下的 CSV 文件
            var lineList = FileIOHelper.ReadFile(path);

            csvFieldModel = GetCSVFieldModel(lineList);

            if (csvFieldModel.CSVFieldList.Count > 0)
            {
                //// 移除CSV文件中非数据的行
                lineList.RemoveRange(0, csvFieldModel.FieldLineIndex);

                var myObject = ElfinTypeBuilder.CreateNewObject(csvFieldModel.CSVFieldList);

                foreach (var line in lineList)
                {
                    var isSC = StringHelper.IsSingleLineComments(line);

                    if (!isSC)
                    {
                        var dataList = line.Split(",").ToList();

                        for (int i = 0; i < dataList.Count; i++)
                        {
                            myObject.GetType().GetProperty(csvFieldModel.CSVFieldList[i].FieldName).SetValue(myObject, dataList[i]);
                        }

                        resutl.Add(myObject);
                    }
                }
            }

            return resutl;
        }

        #region Private Functions

        /// <summary>
        /// 获取 CSV 文件字段集合
        /// </summary>
        /// <param name="lineList">行集合</param>
        /// <returns>CSV文件字段集合</returns>
        private static CSVFieldModel GetCSVFieldModel(List<string> lineList)
        {
            var commentsInfoList = new List<string>();
            var csvFieldModel = new CSVFieldModel();

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
                    var splitNext1LineCount = next1Line.Split(",").Count();
                    var splitNext2LineCount = next2Line.Split(",").Count();

                    //// 当某一行与后两行通过","分割后的节点数量一致，则认定改行为字段名行
                    if (splitLineCount == splitNext1LineCount && splitLineCount == splitNext2LineCount)
                    {
                        csvFieldModel.FieldLineIndex = lineIndex;
                        var fieldNameList = line.Split(",").ToList();

                        foreach (var name in fieldNameList)
                        {
                            csvFieldModel.CSVFieldList.Add(new FieldModel { FieldName = name, FieldType = typeof(string) });
                        }

                        break;
                    }
                }
            }

            return csvFieldModel;
        }

        #endregion
    }
}
