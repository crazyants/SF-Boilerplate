/*******************************************************************************
* 命名空间: SF.Web.Models.Tree
*
* 功 能： N/A
* 类 名： TreeEntity
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2017/1/10 15:08:02 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Models.Tree
{
    /// <summary>
    /// 树实体
    /// </summary>
    public class TreeEntity
    {
        public string parentId { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public int? checkstate { get; set; }
        public bool showcheck { get; set; }
        public bool complete { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool isexpand { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool hasChildren { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string img { get; set; }
        /// <summary>
        /// title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 级
        /// </summary>
        public int? Level { get; set; }
        /// <summary>
        /// 自定义属性
        /// </summary>
        public string Attribute { get; set; }
        /// <summary>
        /// 自定义属性值
        /// </summary>
        public string AttributeValue { get; set; }
        /// <summary>
        /// 自定义属性A
        /// </summary>
        public string AttributeA { get; set; }
        /// <summary>
        /// 自定义属性值A
        /// </summary>
        public string AttributeValueA { get; set; }
    }
}
