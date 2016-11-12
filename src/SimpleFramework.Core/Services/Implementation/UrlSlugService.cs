using System.Linq;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Abstraction.UoW.Helper;

namespace SimpleFramework.Core.Services
{
    public class UrlSlugService : IUrlSlugService
    {
        private readonly IBaseUnitOfWork _baseUnitOfWork;

        public UrlSlugService(IBaseUnitOfWork baseUnitOfWork)
        {
            _baseUnitOfWork = baseUnitOfWork;
        }

        public UrlSlugEntity Get(long entityId, long entityTypeId)
        {
            return _baseUnitOfWork.BaseWorkArea.UrlSlug.Query().FirstOrDefault(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
        }

        public void Add(string slug, long entityId, long entityTypeId)
        {
            var urlSlug = new UrlSlugEntity
            {
                Slug = slug,
                EntityId = entityId,
                EntityTypeId = entityTypeId
            };
            _baseUnitOfWork.ExecuteAndCommit(uow =>
          {
              uow.BaseWorkArea.UrlSlug.Add(urlSlug);
          });

        }

        public void Update(string newName, long entityId, long entityTypeId)
        {
            var urlSlug =
                _baseUnitOfWork.BaseWorkArea.UrlSlug.Query().First(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
            urlSlug.Slug = newName;

            _baseUnitOfWork.ExecuteAndCommit(uow =>
            {
                uow.BaseWorkArea.UrlSlug.Update(urlSlug);
            });
        }

        public void Remove(long entityId, long entityTypeId)
        {
            var urlSlug =
               _baseUnitOfWork.BaseWorkArea.UrlSlug.Query().First(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
            if (urlSlug != null)
            {
                _baseUnitOfWork.ExecuteAndCommit(uow =>
                {
                    uow.BaseWorkArea.UrlSlug.Delete(urlSlug);
                });
            }
        }
    }
}
