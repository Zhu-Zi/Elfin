using System;

namespace Elfin.Core.Models
{
    /// <summary>
    /// 字段模型
    /// </summary>
    public class FieldModel
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public Type FieldType { get; set; }
    }
}
