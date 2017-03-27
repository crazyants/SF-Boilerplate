/*******************************************************************************
* 命名空间: SF.Core.Models
*
* 功 能： N/A
* 类 名： TreeViewItem
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/8 14:21:24 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Models.Tree
{
    public class TreeViewItem
    {
        /// <summary>
        /// 
        /// </summary>
        public enum GetCountsType
        {
            /// <summary>
            /// none
            /// </summary>
            None = 0,

            /// <summary>
            /// child groups
            /// </summary>
            ChildGroups = 1,

            /// <summary>
            /// group members
            /// </summary>
            GroupMembers = 2
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewItem"/> class.
        /// </summary>
        public TreeViewItem()
        {
            IsActive = true;
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has children.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has children; otherwise, <c>false</c>.
        /// </value>
        public bool HasChildren { get; set; }

        /// <summary>
        /// Gets or sets the icon CSS class.
        /// </summary>
        /// <value>
        /// The icon CSS class.
        /// </value>
        public string IconCssClass { get; set; }

        /// <summary>
        /// Gets or sets an image url.
        /// </summary>
        /// <value>
        /// The image url.
        /// </value>
        public string IconSmallUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the count information depending on the setting for GetCountsType
        /// </summary>
        /// <value>
        /// The count information.
        /// </value>
        public int? CountInfo { get; set; }
    }
}
