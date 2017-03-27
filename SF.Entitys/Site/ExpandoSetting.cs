namespace SF.Entitys
{
    /// <summary>
    /// previously we were using a DataTable but DataTable is not supported in .net core
    /// so changed to a list of ExpandoSettings
    /// </summary>
    public class ExpandoSetting
    {
        public ExpandoSetting()
        { }

        public int SiteId { get; set; } = -1;

        private string keyName = string.Empty;
        public string KeyName
        {
            get { return keyName ?? string.Empty; }
            set { keyName = value; }
        }

        private string keyValue = string.Empty;
        public string KeyValue
        {
            get { return keyValue ?? string.Empty; }
            set { keyValue = value; }
        }

        private string groupName = string.Empty;
        public string GroupName
        {
            get { return groupName ?? string.Empty; }
            set { groupName = value; }
        }


        public bool IsDirty { get; set; } = false;

    }
}
