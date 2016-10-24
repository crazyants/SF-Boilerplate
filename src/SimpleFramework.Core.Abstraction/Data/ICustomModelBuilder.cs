using Microsoft.EntityFrameworkCore;

namespace SimpleFramework.Core.Abstraction.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
