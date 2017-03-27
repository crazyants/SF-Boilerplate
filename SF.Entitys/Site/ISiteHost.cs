using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Entitys
{
    public interface ISiteHost
    {
        long Id { get; set; }
        string HostName { get; set; }
        long SiteId { get; set; }

    }
}
