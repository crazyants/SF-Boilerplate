
using SimpleFramework.Infrastructure.Entitys;

namespace SimpleFramework.Core.Settings
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Deep load and populate settings values for entity and all nested objects 
        /// </summary>
        /// <param name="entity"></param>
		void LoadEntitySettingsValues(Entity entity);
        /// <summary>
        /// Deep save entity and all nested objects settings values
        /// </summary>
        /// <param name="entity"></param>
        void SaveEntitySettingsValues(Entity entity);
        /// <summary>
        /// Deep remove entity and all nested objects settings values
        /// </summary>
        /// <param name="entity"></param>
		void RemoveEntitySettings(Entity entity);
		void SaveSettings(SettingEntry[] settings);

        T GetValue<T>(string name, T defaultValue);
        T[] GetArray<T>(string name, T[] defaultValue);
        void SetValue<T>(string name, T value);
    }
}
