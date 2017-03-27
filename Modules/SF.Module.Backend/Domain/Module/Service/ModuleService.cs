
using CacheManager.Core;
using SF.Entitys;
using SF.Module.Backend.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Extensions;
using System.Linq.Expressions;
using LinqKit;
using SF.Entitys.Abstraction.Pages;
using Microsoft.EntityFrameworkCore;
using SF.Module.Backend.Data.Entitys;
using SF.Module.Backend.Data.Uow;
using SF.Core.Abstraction.UoW.Helper;
using Z.EntityFramework.Plus;
using SF.Core.Abstraction.GenericServices;

namespace SF.Module.Backend.Domain.Module.Service
{
    public class ModuleService : ServiceBase, IModuleService
    {
        #region Fields
        private readonly ICacheManager<object> _cacheManager;
        private readonly IBackendUnitOfWork _backendUnitOfWork;
        #endregion

        #region Constructors
        public ModuleService(IBackendUnitOfWork backendUnitOfWork,
            ICacheManager<object> cacheManager)
        {
            _backendUnitOfWork = backendUnitOfWork;
            _cacheManager = cacheManager;

        }
        #endregion


        #region Method  
        public async Task<List<ModuleEntity>> GetAlls()
        {
            return await _cacheManager.GetAsync(ConstHelper.MODULE_ALL, ConstHelper.Region, () =>
           {
               return _backendUnitOfWork.Module.Query().ToListAsync();
           });

        }

        public async Task<List<ModuleEntity>> QueryFilterByParentId(long parentId)
        {
            return (await GetAlls()).Where(e => e.ParentId == parentId).ToList();
        }

        public async Task<List<long>> QueryFilterByParentIds(long[] parentIds)
        {
            return (await GetAlls()).Where(g =>
                  g.ParentId.HasValue &&
                  parentIds.Contains(g.ParentId.Value)).Select(g => g.ParentId.Value)
                    .Distinct()
                    .ToList();
        }

        public async Task<List<ModuleEntity>> GetPageListBykeyword(long parentId, string keyword, string condition)
        {
            var predicate = PredicateBuilder.New<ModuleEntity>();
            predicate.And(d => d.ParentId == parentId);

            #region 多条件查询
            if (!keyword.IsEmpty())
            {
                switch (condition)
                {
                    case "EnCode":        //项目名
                        predicate.And(d => d.EnCode.Contains(keyword));
                        break;
                    case "FullName":      //项目值
                        predicate.And(d => d.FullName.Contains(keyword));
                        break;
                    case "UrlAddress": //拼音
                        predicate.And(d => d.UrlAddress.Contains(keyword));
                        break;
                    default:
                        break;

                }
            }
            #endregion

            return (await GetAlls()).Where(predicate).ToList();
        }

        public async Task<List<PermissionEntity>> GetButtonByModuleId(long moduleId)
        {
            var predicate = PredicateBuilder.New<PermissionEntity>();
            predicate.And(d => d.ModuleId == moduleId);

            return await _backendUnitOfWork.Permission.QueryFilter(predicate).ToListAsync();

        }

        public async Task<List<PermissionEntity>> GetButtonByModuleName(string moduleName = null)
        {
            var predicate = PredicateBuilder.New<PermissionEntity>();
            if (!moduleName.IsEmpty())
            {
                predicate.And(d => d.Name.Contains(moduleName));

            }
            return await _backendUnitOfWork.Permission.QueryFilter(predicate).ToListAsync();

        }

        public void SaveForm(ModuleEntity moduleEntity, List<PermissionEntity> moduleButtonList)
        {
            var module = _backendUnitOfWork.ExecuteAndCommit((uow) =>
            {
                if (moduleEntity.Id != 0)
                {
                    uow.Module.Update(moduleEntity);
                }
                else
                {
                    uow.Module.Add(moduleEntity);
                }
                return moduleEntity;

            });
            if (moduleButtonList != null)
            {
                moduleButtonList.Each(m =>
                      {
                          m.Id = 0;
                          m.ModuleId = module.Id;
                      });
                _backendUnitOfWork.ExecuteAndCommit((uow) =>
                   {
                       //uow.Permission.Set.Where(x => x.Name.Contains(module.EnCode) && x.Id != 0)
                       //.Update(x => new PermissionEntity() { ModuleId = module.Id });
                       uow.Permission.Set.Where(x => x.Name.Contains(module.EnCode) || x.ModuleId == module.Id).Delete();
                       uow.Permission.Add(moduleButtonList);
                   });

            }
        }
        #endregion
    }
}
