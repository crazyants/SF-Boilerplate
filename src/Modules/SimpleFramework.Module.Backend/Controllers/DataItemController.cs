using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Omu.ValueInjecter;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Common;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Models;
using SimpleFramework.Core.Models.Tree;
using SimpleFramework.Core.Web.Base.Controllers;
using SimpleFramework.Core.Web.Base.DataContractMapper;
using SimpleFramework.Module.Backend.Services;
using SimpleFramework.Module.Backend.ViewModels;
using SimpleFramework.Web.Control.JqGrid.Core.Json;
using SimpleFramework.Web.Control.JqGrid.Core.Request;
using SimpleFramework.Web.Control.JqGrid.Core.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        private readonly IRepositoryAsync<DataItemEntity> _repositoryAsync;
        public DataItemController(IServiceCollection collection, ILogger<DataItemController> logger,
           IRepositoryAsync<DataItemEntity> repositoryAsync)
            : base(collection, logger)
        {
            this._repositoryAsync = repositoryAsync;
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

            var qry = _repositoryAsync.GetChildren(id, rootDataItemId);

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
                    treeViewItem.CountInfo = _repositoryAsync.Queryable().Where(a => a.ParentId.HasValue && a.ParentId == group.Id).Count();
                }

                groupNameList.Add(treeViewItem);

            }

            //快速找出哪些项目有子级
            List<long> resultIds = dataItemEntityList.Select(a => a.Id).ToList();
            var qryHasChildrenList = _repositoryAsync.Queryable()
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
