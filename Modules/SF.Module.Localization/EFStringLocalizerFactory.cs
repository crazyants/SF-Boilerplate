using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SF.Core.Abstraction.Data;
using SF.Module.Localization.Models;
using SF.Module.Localization.Data;

namespace SF.Module.Localization
{
    public class EfStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IResourceUnitOfWork _unitOfWork;
        private IList<ResourceString> _resourceStrings;

        public EfStringLocalizerFactory(IResourceUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            LoadResources();
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EfStringLocalizer(_resourceStrings);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EfStringLocalizer(_resourceStrings);
        }

        private void LoadResources()
        {
            _resourceStrings = _unitOfWork.Resource.Query().Include(x => x.Culture).Select(x => new ResourceString
            {
                Culture = x.Culture.Name,
                Key = x.Key,
                Value = x.Value
            }).ToList();
        }
    }
}