using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elfin.IO.Files
{
    /// <summary>
    /// 文件IO辅助类
    /// </summary>
    public class FileIOHelper
    {
        private static List<string> _pathList = new List<string>();

        #region Get Functions

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
                _pathList.Add(item);
            }

            foreach (var item in data)
            {
                GetAllSubdirectoryByPath(item);
            }

            return _pathList;
        }

        /// <summary>
        /// 获取指定路径下的全部文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="searchPattern">搜索模式</param>
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

        #endregion

        #region Create Functions

        /// <summary>
        /// Create target file
        /// 创建目标文件
        /// </summary>
        /// <param name="folder">folder</param>
        /// <param name="fileName">folder name</param>
        /// <param name="fileExtension">file extension</param>
        /// <returns>file path</returns>
        public static string CreateFile(string folder, string fileName, string fileExtension)
        {
            string filePath = $"{folder}\\{fileName}.{fileExtension}";

            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return filePath;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="folder">folder</param>
        /// <returns></returns>
        public static bool CreateDirectory(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        #endregion

        #region Read Functions

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static List<string> ReadFile(string path)
        {
            var linelist = new List<string>();
            var sr = new StreamReader(path, Encoding.Default);
            var line = "";

            while (!string.IsNullOrEmpty(line = sr.ReadLine()))
            {
                linelist.Add(line);
            }

            sr.Close();

            return linelist;
        }

        #endregion

        #region Other Functions

        /// <summary>
        /// 复制文件到指定文件夹
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="desinationPath"></param>
        /// <returns></returns>
        public static bool CopyTo(string sourcePath, string desinationPath, bool overwrite = true)
        {
            try
            {
                File.Copy(sourcePath, desinationPath, overwrite);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CopyTo] {ex}");
                return false;
            }
        }

        /// <summary>
        /// 压缩文件为 zip
        /// </summary>
        /// <param name="folderPath">
        /// 待压缩文件夹路径
        /// eg:..\\foldername
        /// </param>
        /// <param name="zipPath">
        /// 压缩文件存放路径
        /// eg: ..\\Test.zip
        /// </param>
        /// <returns>是否压缩成功</returns>
        public static bool CompressFileToZip(string folderPath, string zipPath)
        {
            var isSuccess = true;
            var quit = false;

            try
            {
                Console.WriteLine("[Run]...");

                Task.Run(() =>
                {
                    while (!quit)
                    {
                        Console.WriteLine($"[DateTime] {DateTime.UtcNow:HH:mm:ss}");
                        Thread.Sleep(1000);
                    }
                });

                var lastSubdirectory = zipPath.Split(@"\").Last();
                var directoryPath = zipPath.Replace(lastSubdirectory, "");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var startTime = DateTime.UtcNow;
                ZipFile.CreateFromDirectory(folderPath, zipPath, CompressionLevel.Fastest, false);
                var endTime = DateTime.UtcNow;
                isSuccess = true;
                quit = true;

                Console.WriteLine("[Success]");
                Console.WriteLine($"[UserTime] {(endTime - startTime).TotalMinutes} min");
            }
            catch (Exception ex)
            {
                isSuccess = false;
                quit = true;
                throw ex;
            }

            return isSuccess;
        }

        #endregion
    }
}
