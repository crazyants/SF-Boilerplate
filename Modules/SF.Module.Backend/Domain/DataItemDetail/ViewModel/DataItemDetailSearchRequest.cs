using SF.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.Backend.Domain.DataItemDetail.ViewModel
{
    public class DataItemDetailSearchRequest: EntityModelBase
    {
        public string Condition { get; set; }
        public string KeyWord { get; set; }
        public long ItemId { get; set; }
 
    }
}
