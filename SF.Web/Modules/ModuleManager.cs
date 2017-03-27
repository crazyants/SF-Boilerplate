using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Linq;
using SF.Core.Common;
using CacheManager.Core;
using SF.Core.Extensions;
using SF.Data.MigratorHelper;
using SF.Core.Infrastructure.Modules.Builder;
using SF.Core.Infrastructure.Modules;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using SF.Web.Modules.Data;
using SF.Core.Abstraction.UoW.Helper;
using SF.Core;

namespace SF.Web.Modules
{
    public class ModuleManager : IModuleManager
    {
        private ILogger<ModuleManager> _logger;
        private IModuleConfigBuilder _builder;
        private readonly ICacheManager<object> _cacheManager;
        private IModulesUnitOfWork _unitOfWork;
        private ApplicationPartManager _appPartManager;

        private const string cacheKey = "moduleInfo";

        public ModuleManager(IServiceProvider serviceProvider,
            IModuleConfigBuilder builder,
            ApplicationPartManager appPartManager,
            ICacheManager<object> cacheManager,
            IModulesUnitOfWork unitOfWork)
        {
            this._logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<ModuleManager>();
            this._builder = builder;
            this._cacheManager = cacheManager;
            this._appPartManager = appPartManager;
            this._unitOfWork = unitOfWork;
        }


        public List<ModuleInfo> GetModule()
        {
            return ExtensionManager.Modules;
        }
        /// <summary>
        /// 安装所有模块
        /// </summary>
        public void InstallModule(ModuleInfo moduleInfo)
        {

            InstallModuleMigrate(moduleInfo);
            var installedModule = _unitOfWork.Module.Query().FirstOrDefault(x => x.ModuleAssemblyName == moduleInfo.Name);
            if (installedModule == null)
            {
                installedModule = new InstalledModule
                {
                    Active = false,
                    DateInstalled = DateTime.UtcNow,
                    Installed = true,
                    ModuleAssemblyName = moduleInfo.Name,
                    ModuleName = moduleInfo.Name,
                    ModuleVersion = moduleInfo.Config.Version,
                };
                _unitOfWork.ExecuteAndCommit(uow =>
                {
                    uow.Module.Add(installedModule);
                });
            }
            else
            {
                installedModule.Active = true;
                installedModule.DateActivated = DateTime.UtcNow;
                _unitOfWork.ExecuteAndCommit(uow =>
                {
                    return uow.Module.Update(installedModule);
                });
            }
            RegisterAssemblyInPartManager(moduleInfo);

        }
        /// <summary>
        /// 卸载所有模块
        /// </summary>
        public void UnInstallModule(ModuleInfo moduleInfo)
        {

            //系统模块不能卸载
            if (moduleInfo.Config.SystemModule) return;

            UnInstallModuleMigrate(moduleInfo);
            var installedModule = _unitOfWork.Module.Query().First(x => x.ModuleAssemblyName == moduleInfo.Name);
            if (installedModule == null)
                throw new NullReferenceException($"Can not find installed Module for {moduleInfo.Name}");

            installedModule.Active = false;
            installedModule.DateDeactivated = DateTime.UtcNow;

            _unitOfWork.ExecuteAndCommit(uow =>
            {
                return uow.Module.Update(installedModule);
            });
            RemoveFromAssemblyInPartManager(moduleInfo);

        }

        /// <summary>
        /// 安装某模块
        /// </summary>
        /// <param name="moduleKey">模块Key</param>
        private void InstallModuleMigrate(ModuleInfo module)
        {

            if (!module.Config.ConnectionString.IsEmpty())
            {
                var migrationsWrapper = new MigrationsWrapper(module.Config.ConnectionString, module.Assembly, (str) =>
                {
                    this._logger.LogInformation(str);
                });
                migrationsWrapper.MigrateToLatestVersion();
            }

        }
        /// <summary>
        /// 卸载某模块
        /// </summary>
        /// <param name="moduleKey">模块Key</param>
        private void UnInstallModuleMigrate(ModuleInfo module)
        {

            //系统模块不能卸载
            if (module.Config.SystemModule) return;

            if (!module.Config.ConnectionString.IsEmpty())
            {
                var migrationsWrapper = new MigrationsWrapper(module.Config.ConnectionString, module.Assembly, (str) =>
                {
                    this._logger.LogInformation(str);
                });
                migrationsWrapper.MigrateCallback(0);
            }
        }

        private void RegisterAssemblyInPartManager(ModuleInfo module)
        {
            _appPartManager.ApplicationParts.Add(new AssemblyPart(module.Assembly));
        }

        private void RemoveFromAssemblyInPartManager(ModuleInfo module)
        {

            var assemblyPart = new AssemblyPart(module.Assembly);
            var assmblyPartFromMgr = _appPartManager.ApplicationParts.FirstOrDefault(a => a.Name == assemblyPart.Name);

            if (assmblyPartFromMgr == null)
                throw new NullReferenceException($"Can not find the assembly { module.Name} in ApplicationPartManager");

            _appPartManager.ApplicationParts.Remove(assmblyPartFromMgr);

        }

    }
}
