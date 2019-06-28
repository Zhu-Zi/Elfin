using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// 数据写入CSV文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath">文件路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="dataList">数据集合</param>
        /// <returns>方法是否执行成功</returns>
        public static bool WriteCSVFile<T>(string folderPath, string fileName, List<T> dataList)
        {
            var isSuccess = false;

            if (!string.IsNullOrEmpty(folderPath) && !string.IsNullOrEmpty(fileName) && dataList != null && dataList.Count > 0)
            {
                var filePath = FileIOHelper.CreateFile(folderPath, fileName, "csv");
                isSuccess = WriteCSV(filePath, dataList);
            }

            return isSuccess;
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

        /// <summary>
        /// 写CSV方法
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="filePath">CSV文件路径</param>
        /// <param name="dataList">数据集合</param>
        /// <returns></returns>
        private static bool WriteCSV<T>(string filePath, List<T> dataList)
        {
            var isSuccess = false;
            StreamWriter sw = null;

            try
            {
                var typeList = typeof(T).GetProperties();
                var lastType = typeList.Last();
                var strColumn = new StringBuilder();
                var strValue = new StringBuilder();

                sw = new StreamWriter(filePath);

                //// 设置列头
                foreach (var type in typeList)
                {
                    strColumn.Append(type.Name);

                    if (type != lastType)
                    {
                        strColumn.Append(",");
                    }
                }

                strColumn.Remove(strColumn.Length - 1, 1);
                sw.WriteLine(strColumn);//// 写入列名

                //// 按行写入数据
                foreach (var item in dataList)
                {
                    strValue.Remove(0, strValue.Length);//// 清除临时行值 clear the temp row value

                    foreach (var type in typeList)
                    {
                        strValue.Append(item.GetType().GetProperty(type.Name).GetValue(item));

                        if (type != lastType)
                        {
                            strValue.Append(",");
                        }
                    }

                    sw.WriteLine(strValue);//// 写入行数据
                }

                isSuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sw != null)
                {
                    sw.Dispose();
                }
            }

            return isSuccess;
        }

        #endregion
    }
}
