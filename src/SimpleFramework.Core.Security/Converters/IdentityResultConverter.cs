using System.Linq;
using Omu.ValueInjecter;
using Microsoft.AspNetCore.Identity;
using SimpleFramework.Core.Security;

namespace SimpleFramework.Core.Security.Converters
{
    public static class IdentityResultConverter
    {
        public static SecurityResult ToCoreModel(this IdentityResult dataModel)
        {
            var result = new SecurityResult();
            result.InjectFrom(dataModel);

            if (dataModel.Errors != null)
                result.Errors = dataModel.Errors.Select(x=>x.Description).ToArray();

            return result;
        }
    }
}
