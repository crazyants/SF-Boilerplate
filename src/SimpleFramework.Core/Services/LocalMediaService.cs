using System.IO;
using SimpleFramework.Core.Abstraction;
using SimpleFramework.Core.Abstraction.Data;
using SimpleFramework.Core.Entitys;

namespace SimpleFramework.Core.Services
{
    public class LocalMediaService : IMediaService
    {
        private const string MediaRootFoler = "user-content";

        private IRepository<MediaEntity> _mediaRespository;

        public LocalMediaService(IRepository<MediaEntity> mediaRespository)
        {
            this._mediaRespository = mediaRespository;
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
            _mediaRespository.Delete(media);
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
