using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MiniDefinition.EntityFrameworkCore;

[DependsOn(
    typeof(MiniDefinitionDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class MiniDefinitionEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MiniDefinitionDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
