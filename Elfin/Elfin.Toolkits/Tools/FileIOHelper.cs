using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Elfin.Toolkits.Tools
{
    /// <summary>
    /// 文件输入输出辅助类
    /// </summary>
    public class FileIOHelper
    {
        private static List<string> _allSubdirectoryPathList = new List<string>();

        /// <summary>
        /// 获取指定路径下的全部文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="searchPattern">搜索模式（Eg:"*.csv"）</param>
        /// <returns></returns>
        public static List<string> GetAllFilesByPath(string path, string searchPattern = "*")
        {
            var result = new List<string>();
            var data = Directory.GetFiles(path, searchPattern);

            if (data.Length > 0)
            {
                result = data.ToList();
            }

            return result;
        }

        /// <summary>
        /// 通过路径递归获取该路径下的所有子目录
        /// </summary>
        /// <param name="rootPath">路径</param>
        /// <returns>路径下的全部子目录合集</returns>
        public static List<string> GetAllSubdirectoryByPath(string rootPath)
        {
            var data = Directory.GetDirectories(rootPath);

            foreach (var item in data)
            {
                _allSubdirectoryPathList.Add(item);
            }

            foreach (var item in data)
            {
                GetAllSubdirectoryByPath(item);
            }

            return _allSubdirectoryPathList;
        }
    }
}
