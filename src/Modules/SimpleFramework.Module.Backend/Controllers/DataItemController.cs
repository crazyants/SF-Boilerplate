using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleFramework.Core.Common;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Extensions;
using SimpleFramework.Core.Web.Models.Tree;
using SimpleFramework.Module.Backend.Services;
using System.Collections.Generic;
using System.Linq;


namespace SimpleFramework.Module.Backend.Controllers
{
    /// <summary>
    /// 字典管理
    /// </summary>
    [Authorize]
    [Route("DataItem/")]
    public class DataItemController : Core.Web.Base.Controllers.ControllerBase
    {

        private readonly IBaseUnitOfWork _baseUnitOfWork;
        public DataItemController(IServiceCollection collection, ILogger<DataItemController> logger,
           IBaseUnitOfWork baseUnitOfWork)
            : base(collection, logger)
        {
            this._baseUnitOfWork = baseUnitOfWork;
        }
        public ActionResult Index()
        {
            return View();
        }

        [Route("GetChildren/{id}")]
        public IQueryable<TreeViewItem> GetChildren(
          int id,
          int rootDataItemId = 0,
          TreeViewItem.GetCountsType countsType = TreeViewItem.GetCountsType.None)
        {
           
            var qry = this._baseUnitOfWork.BaseWorkArea.DataItem.GetChildren(id, rootDataItemId);

            List<DataItemEntity> dataItemEntityList = new List<DataItemEntity>();
            List<TreeViewItem> groupNameList = new List<TreeViewItem>();

            foreach (var group in qry.OrderBy(g => g.ItemName))
            {

                dataItemEntityList.Add(group);
                var treeViewItem = new TreeViewItem();
                treeViewItem.Id = group.Id.ToString();
                treeViewItem.Name = group.ItemName;
                treeViewItem.IsActive = (group.EnabledMark??0)>0;

                treeViewItem.IconCssClass = "fa fa-sitemap";

                if (countsType == TreeViewItem.GetCountsType.ChildGroups)
                {
                    treeViewItem.CountInfo = this._baseUnitOfWork.BaseWorkArea.DataItem.Query().Where(a => a.ParentId.HasValue && a.ParentId == group.Id).Count();
                }

                groupNameList.Add(treeViewItem);

            }

            //快速找出哪些项目有子级
            List<long> resultIds = dataItemEntityList.Select(a => a.Id).ToList();
            var qryHasChildrenList = this._baseUnitOfWork.BaseWorkArea.DataItem.Query()
                .Where(g =>
                   g.ParentId.HasValue &&
                   resultIds.Contains(g.ParentId.Value)).Select(g => g.ParentId.Value)
                .Distinct()
                .ToList();

            foreach (var g in groupNameList)
            {
                int groupId = g.Id.AsInteger();
                g.HasChildren = qryHasChildrenList.Any(a => a == groupId);
            }

            return groupNameList.AsQueryable();
        }
    }

}
