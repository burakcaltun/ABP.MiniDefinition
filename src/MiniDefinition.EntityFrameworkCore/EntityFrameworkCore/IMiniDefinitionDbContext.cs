using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MiniDefinition.EntityFrameworkCore;

[ConnectionStringName(MiniDefinitionDbProperties.ConnectionStringName)]
public interface IMiniDefinitionDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
