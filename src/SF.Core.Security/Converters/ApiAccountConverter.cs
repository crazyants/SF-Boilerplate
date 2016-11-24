using System;
using Omu.ValueInjecter;
using SF.Core.Security;
using SF.Core.Entitys;

namespace SF.Core.Security.Converters
{
    public static class ApiAccountConverter
    {
        public static ApiAccount ToCoreModel(this ApiAccountEntity entity)
        {
            var result = new ApiAccount();
            result.InjectFrom(entity);

            result.ApiAccountType = entity.ApiAccountType;
            result.IsActive = entity.IsActive;

            return result;
        }

        public static ApiAccountEntity ToDataModel(this ApiAccount model)
        {
            var result = new ApiAccountEntity();
            result.InjectFrom(model);
 
            result.Id = model.Id;
 
            if (model.IsActive != null)
            {
                result.IsActive = model.IsActive.Value;
            }

            result.ApiAccountType = model.ApiAccountType;

            return result;
        }

        /// <summary>
        /// Patch changes
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void Patch(this ApiAccountEntity source, ApiAccountEntity target)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            //  var patchInjection = new PatchInjection<ApiAccountEntity>(x => x.Name, x => x.ApiAccountType, x => x.IsActive, x => x.SecretKey, x => x.AppId);
            //  target.InjectFrom(patchInjection, source);
        }
    }
}
