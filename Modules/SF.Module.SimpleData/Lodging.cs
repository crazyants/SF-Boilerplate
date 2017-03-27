using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleData
{
    /// <summary>
    /// 住宿类
    /// </summary>    
    public class Lodging
    {
        public int LodgingId { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public bool IsResort { get; set; }  //是否度假胜地

        public Destination Destination { get; set; }
        public struct aa { }
    }

    /// <summary>
    /// 景点类
    /// </summary>
    public class Destination
    {
        public int DestinationId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }

        public List<Lodging> Lodgings { get; set; }
    }
}
