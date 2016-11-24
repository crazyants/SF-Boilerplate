
using SF.Core.Abstraction.Entitys;

namespace SF.Core.Settings
{
    public interface ISettingsManager
    {
        /// <summary>
        /// Deep load and populate settings values for entity and all nested objects 
        /// </summary>
        /// <param name="entity"></param>
		void LoadEntitySettingsValues(BaseEntity entity);
        /// <summary>
        /// Deep save entity and all nested objects settings values
        /// </summary>
        /// <param name="entity"></param>
        void SaveEntitySettingsValues(BaseEntity entity);
        /// <summary>
        /// Deep remove entity and all nested objects settings values
        /// </summary>
        /// <param name="entity"></param>
		void RemoveEntitySettings(BaseEntity entity);
		void SaveSettings(SettingEntry[] settings);

        T GetValue<T>(string name, T defaultValue);
        T[] GetArray<T>(string name, T[] defaultValue);
        void SetValue<T>(string name, T value);
    }
}
