using MiniDefinition.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MiniDefinition;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(MiniDefinitionEntityFrameworkCoreTestModule)
    )]
public class MiniDefinitionDomainTestModule : AbpModule
{

}
