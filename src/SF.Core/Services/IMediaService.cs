using System.IO;
using SF.Core.Entitys;

namespace SF.Core.Services
{
    public interface IMediaService
    {
        string GetMediaUrl(MediaEntity media);

        string GetMediaUrl(string fileName);

        string GetThumbnailUrl(MediaEntity media);

        void SaveMedia(Stream mediaBinaryStream, string fileName, string mimeType = null);

        void DeleteMedia(MediaEntity media);

        void DeleteMedia(string fileName);
    }
}
