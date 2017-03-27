
using SF.Entitys.Abstraction;
using System;

namespace SF.Entitys
{
    public class SiteHost : BaseEntity<long>, ISiteHost
    {
        public SiteHost()
        {
            
        }
        

        private string hostName = string.Empty;
        public string HostName
        {
            get { return hostName ?? string.Empty; }
            set { hostName = value; }
        }

        public long SiteId { get; set; }
        
    }
}
