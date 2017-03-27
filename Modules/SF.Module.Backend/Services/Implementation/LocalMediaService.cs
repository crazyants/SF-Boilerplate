using System.IO;
using SF.Core.Abstraction;
using SF.Core.Abstraction.Data;
using SF.Entitys;
using SF.Data;
using SF.Core.Abstraction.UoW;
using SF.Core.Abstraction.UoW.Helper;
using SF.Core;
using SF.Core.Abstraction.GenericServices;

namespace SF.Module.Backend.Services.Implementation
{
    public class LocalMediaService : ServiceBase, IMediaService
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
