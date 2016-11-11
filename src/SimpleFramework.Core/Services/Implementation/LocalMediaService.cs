using System.IO;
using SimpleFramework.Core.Abstraction;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Abstraction.UoW;
using SimpleFramework.Core.Abstraction.UoW.Helper;

namespace SimpleFramework.Core.Services
{
    public class LocalMediaService : IMediaService
    {
        private const string MediaRootFoler = "user-content";

        private IBaseUnitOfWork _baseUnitOfWork;



        public LocalMediaService(IBaseUnitOfWork baseUnitOfWork)
        {
            this._baseUnitOfWork = baseUnitOfWork;
        }

        public string GetMediaUrl(MediaEntity media)
        {
            if (media != null)
            {
                return $"/{MediaRootFoler}/{media.FileName}";
            }

            return $"/{MediaRootFoler}/no-image.png";
        }

        public string GetMediaUrl(string fileName)
        {
            return $"/{MediaRootFoler}/{fileName}";
        }

        public string GetThumbnailUrl(MediaEntity media)
        {
            return GetMediaUrl(media);
        }

        public void SaveMedia(Stream mediaBinaryStream, string fileName, string mimeType = null)
        {
            var filePath = Path.Combine(GlobalConfiguration.WebRootPath, MediaRootFoler, fileName);
            using (var output = new FileStream(filePath, FileMode.Create))
            {
                mediaBinaryStream.CopyTo(output);
            }
        }

        public void DeleteMedia(MediaEntity media)
        {
            this._baseUnitOfWork.ExecuteAndCommit(uow => uow.BaseWorkArea.Media.Delete(media));
            //this._factory.GetAndReleaseAfterExecuteAndCommit<IUnitOfWorkFactory, IBaseUnitOfWork, MediaEntity>(
            //      uow => uow.BaseWorkArea.Media.Delete(media));
            DeleteMedia(media.FileName);
        }

        public void DeleteMedia(string fileName)
        {
            var filePath = Path.Combine(GlobalConfiguration.WebRootPath, MediaRootFoler, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
