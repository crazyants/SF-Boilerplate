using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Module.SimpleData
{
    public class UnicornsContext : DbContext
    {
        public UnicornsContext(DbContextOptions options) : base(options)
        // C# will call base class parameterless constructor by default
        {

        }
        //以下是数据库上下文对象，以后对数据库的访问就用下面对象
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Lodging> Lodgings { get; set; }

 

    }

}
