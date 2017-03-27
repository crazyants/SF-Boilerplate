using System.IO;
using SF.Entitys;
using SF.Core.Abstraction.GenericServices;

namespace SF.Module.Backend.Services
{
    public interface IMediaService : IServiceBase
    {
        string GetMediaUrl(MediaEntity media);

        string GetMediaUrl(string fileName);

        string GetThumbnailUrl(MediaEntity media);

        void SaveMedia(Stream mediaBinaryStream, string fileName, string mimeType = null);

        void DeleteMedia(MediaEntity media);

        void DeleteMedia(string fileName);
    }
}
