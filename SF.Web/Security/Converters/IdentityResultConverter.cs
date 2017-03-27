using System.Linq;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace SF.Web.Security.Converters
{
    public static class IdentityResultConverter
    {
        public static SecurityResult ToCoreModel(this IdentityResult dataModel)
        {
            var result = new SecurityResult();
            result = Mapper.Map<IdentityResult, SecurityResult>(dataModel);
            if (dataModel.Errors != null)
                result.Errors = dataModel.Errors.Select(x=>x.Description).ToArray();

            return result;
        }
    }
}
