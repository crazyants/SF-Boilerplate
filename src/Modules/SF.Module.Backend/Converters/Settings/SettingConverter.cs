
using webModel = SF.Module.Backend.ViewModels.Setting;
using moduleModel = SF.Core.Settings;
using AutoMapper;

namespace SF.Module.Backend.Converters.Settings
{
    public static class SettingConverter
    {
        public static webModel.SettingViewModel ToWebModel(this moduleModel.SettingEntry setting)
        {
			var retVal = new webModel.SettingViewModel();
	 
            retVal = Mapper.Map<moduleModel.SettingEntry, webModel.SettingViewModel>(setting);
            return retVal;
        }

		public static moduleModel.SettingEntry ToModuleModel(this webModel.SettingViewModel setting)
        {
			var retVal = new moduleModel.SettingEntry();
		 
            retVal = Mapper.Map <webModel.SettingViewModel,moduleModel.SettingEntry>(setting);
            return retVal;
        }
    }
}
