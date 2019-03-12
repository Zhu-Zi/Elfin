using System;
using System.Collections.Generic;
using System.Text;
using Elfin.Core.Models;

namespace Elfin.IO.Models
{
    /// <summary>
    /// CSV文件字段模型
    /// </summary>
    public class CSVFieldModel
    {
        public CSVFieldModel()
        {
            CSVFieldList = new List<FieldModel>();
        }

        /// <summary>
        /// 字段所在行索引
        /// </summary>
        public int FieldLineIndex { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<FieldModel> CSVFieldList { get; set; }
    }
}
