using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CacheManager.Core;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Abstraction.Entitys;
using Microsoft.EntityFrameworkCore;
using SimpleFramework.Core.Settings.Converters;
using SimpleFramework.Core.Common;
using SimpleFramework.Core.Extensions;

namespace SimpleFramework.Core.Settings
{
    public class SettingsManager : ISettingsManager
    {
        private readonly IRepository<SettingEntity> _repository;
        private readonly ICacheManager<object> _cacheManager;
        private readonly IDictionary<string, List<SettingEntry>> _runtimeModuleSettingsMap = new Dictionary<string, List<SettingEntry>>();

        public SettingsManager(IRepository<SettingEntity> repository, ICacheManager<object> cacheManager)
        {
            _repository = repository;
            _cacheManager = cacheManager;
        }

        #region ISettingsManager Members


        public void LoadEntitySettingsValues(EntityBase entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");


            var storedSettings = new List<SettingEntry>();
            var entityType = entity.GetType().Name;

            var settings = _repository.Queryable()
                .Include(s => s.SettingValues)
                .Where(x => x.Id == entity.Id && x.ObjectType == entityType)
                .OrderBy(x => x.Name)
                .ToList();

            storedSettings.AddRange(settings.Select(x => x.ToModel()));

            //Deep load settings values for all object contains settings
            var haveSettingsObjects = entity.GetFlatObjectsListWithInterface<IHaveSettings>();
            foreach (var haveSettingsObject in haveSettingsObjects)
            {
                // Replace settings values with stored in database
                if (haveSettingsObject.Settings != null)
                {
                    //Need clone settings entry because it may be shared for multiple instances
                    haveSettingsObject.Settings = haveSettingsObject.Settings.Select(x => (SettingEntry)x.Clone()).ToList();

                    foreach (var setting in haveSettingsObject.Settings)
                    {
                        var storedSetting = storedSettings.FirstOrDefault(x => String.Equals(x.Name, setting.Name, StringComparison.CurrentCultureIgnoreCase));
                        //First try to used stored object setting values
                        if (storedSetting != null)
                        {
                            setting.Value = storedSetting.Value;
                            setting.ArrayValues = storedSetting.ArrayValues;
                        }
                        else if (setting.Value == null && setting.ArrayValues == null)
                        {
                          
                        }

                    }
                }
            }
        }

        public void SaveEntitySettingsValues(EntityBase entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            var objectType = entity.GetType().Name;

            var haveSettingsObjects = entity.GetFlatObjectsListWithInterface<IHaveSettings>();

            foreach (var haveSettingsObject in haveSettingsObjects)
            {
                var settings = new List<SettingEntry>();

                if (haveSettingsObject.Settings != null)
                {
                    //Save settings
                    foreach (var setting in haveSettingsObject.Settings)
                    {
                        setting.Id = entity.Id;
                        setting.ObjectType = objectType;
                        settings.Add(setting);
                    }
                }
                SaveSettings(settings.ToArray());
            }
        }

        public void RemoveEntitySettings(EntityBase entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (entity == null)
                throw new ArgumentNullException("entity transistent");

            var objectType = entity.GetType().Name;
           
                var settings = _repository.Queryable().Include(s => s.SettingValues)
                                                  .Where(x => x.Id == entity.Id && x.ObjectType == objectType).ToList();
                foreach (var setting in settings)
                {
                    _repository.Delete(setting);
                }
               
            

        }

        public void SaveSettings(SettingEntry[] settings)
        {
            if (settings != null && settings.Any())
            {
                var settingKeys = settings.Select(x => String.Join("-", x.Name, x.ObjectType, x.Id)).Distinct().ToArray();


                using (var changeTracker = new ObservableChangeTracker())
                {
                    var alreadyExistSettings = _repository.Queryable()
                        .Include(s => s.SettingValues)
                        .Where(x => settingKeys.Contains(x.Name + "-" + x.ObjectType + "-" + x.Id))
                        .ToList();

                    changeTracker.AddAction = x => _repository.Insert((SettingEntity)x);
                    //Need for real remove object from nested collection (because EF default remove references only)
                    changeTracker.RemoveAction = x => _repository.Delete((SettingEntity)x);

                    var target = new { Settings = new ObservableCollection<SettingEntity>(alreadyExistSettings) };
                    var source = new { Settings = new ObservableCollection<SettingEntity>(settings.Select(x => x.ToEntity())) };

                    changeTracker.Attach(target);
                    var settingComparer = AnonymousComparer.Create((SettingEntity x) => String.Join("-", x.Name, x.ObjectType, x.Id));
                    source.Settings.Patch(target.Settings, settingComparer, (sourceSetting, targetSetting) => sourceSetting.Patch(targetSetting));
                }

                ClearCache();
            }
        }

        public T[] GetArray<T>(string name, T[] defaultValue)
        {
            var result = defaultValue;

            var repositorySetting = GetAllEntities()
                .FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));

            if (repositorySetting != null)
            {
                result = repositorySetting.SettingValues
                    .Select(v => v.RawValue())
                    .Where(rv => rv != null)
                    .Select(rv => (T)rv)
                    .ToArray();
            }

            return result;
        }

        public T GetValue<T>(string name, T defaultValue)
        {
            var result = defaultValue;

            var values = GetArray(name, new[] { defaultValue });

            if (values.Any())
            {
                result = values.First();
            }

            return result;
        }

        public void SetValue<T>(string name, T value)
        {
            var setting = name.ToModel(value);
            SaveSettings(new[] { setting });
        }

        #endregion

        private List<SettingEntity> GetAllEntities()
        {
            var result = _cacheManager.Get("AllSettings", "SimpleFrameworkRegion", LoadAllEntities);
            return result;
        }

        private List<SettingEntity> LoadAllEntities()
        {
            return _repository.Queryable()
                    .Where(x => x.ObjectType == null )
                    .Include(s => s.SettingValues)
                    .ToList();

        }

        private void ClearCache()
        {
            _cacheManager.Remove("AllSettings", "SimpleFrameworkRegion");
        }
    }
}
