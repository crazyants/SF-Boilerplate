using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Module.Localization.Models;
using SimpleFramework.Module.Localization.Data;

namespace SimpleFramework.Module.Localization
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