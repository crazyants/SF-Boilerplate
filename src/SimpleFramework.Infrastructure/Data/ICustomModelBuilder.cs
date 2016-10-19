using Microsoft.EntityFrameworkCore;

namespace SimpleFramework.Infrastructure.Data
{
    public interface ICustomModelBuilder
    {
        void Build(ModelBuilder modelBuilder);
    }
}
