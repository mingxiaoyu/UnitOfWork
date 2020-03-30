using Microsoft.EntityFrameworkCore;

namespace Mingxiaoyu.Microsoft.EntityFrameworkCore
{
    public interface IMappingConfiguration
    {
        void ApplyConfiguration(ModelBuilder modelBuilder);
    }
}