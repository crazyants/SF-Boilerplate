using Omu.ValueInjecter;
using webModel = SF.Module.Backend.ViewModels.Setting;
using moduleModel = SF.Core.Settings;

namespace SF.Module.Backend.Converters.Settings
{
    public static class SettingConverter
    {
        public static webModel.SettingViewModel ToWebModel(this moduleModel.SettingEntry setting)
        {
			var retVal = new webModel.SettingViewModel();
			retVal.InjectFrom(setting);
            return retVal;
        }

		public static moduleModel.SettingEntry ToModuleModel(this webModel.SettingViewModel setting)
        {
			var retVal = new moduleModel.SettingEntry();
			retVal.InjectFrom(setting);
			return retVal;
        }
    }
}
