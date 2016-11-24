using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Dapper.Attributes
{
    /// <summary>
    /// 忽略属性的数据库属性
    /// </summary>
    public class DBIgnoreAttribute : Attribute
    {
        public DBIgnoreAttribute()
        {
            this.IgnoreMember = true;
        }

        public bool IgnoreMember { get; set; }
    }
}
