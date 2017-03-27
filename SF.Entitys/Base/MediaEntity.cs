using SF.Entitys.Abstraction;

namespace SF.Entitys
{
    public class MediaEntity : BaseEntity
    {
        public string Caption { get; set; }

        public int FileSize { get; set; }

        public string FileName { get; set; }

        public MediaType MediaType { get; set; }
    }
}
