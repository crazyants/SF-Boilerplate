using System.Linq;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Services
{
    public class UrlSlugService : IUrlSlugService
    {
        private readonly IRepository<UrlSlugEntity> _urlSlugRepository;

        public UrlSlugService(IRepository<UrlSlugEntity> urlSlugRepository)
        {
            _urlSlugRepository = urlSlugRepository;
        }

        public UrlSlugEntity Get(long entityId, long entityTypeId)
        {
            return _urlSlugRepository.Queryable().FirstOrDefault(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
        }

        public void Add(string slug, long entityId, long entityTypeId)
        {
            var urlSlug = new UrlSlugEntity
            {
                Slug = slug,
                EntityId = entityId,
                EntityTypeId = entityTypeId
            };

            _urlSlugRepository.Insert(urlSlug);
        }

        public void Update(string newName, long entityId, long entityTypeId)
        {
            var urlSlug =
                _urlSlugRepository.Queryable().First(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
            urlSlug.Slug = newName;
        }

        public void Remove(long entityId, long entityTypeId)
        {
            var urlSlug =
               _urlSlugRepository.Queryable().FirstOrDefault(x => x.EntityId == entityId && x.EntityTypeId == entityTypeId);
            if (urlSlug != null)
            {
                _urlSlugRepository.Delete(urlSlug);
            }
        }
    }
}
