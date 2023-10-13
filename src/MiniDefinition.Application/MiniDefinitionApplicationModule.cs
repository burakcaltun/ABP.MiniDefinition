using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;

namespace MiniDefinition;

[DependsOn(
    typeof(MiniDefinitionDomainModule),
    typeof(MiniDefinitionApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
public class MiniDefinitionApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<MiniDefinitionApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MiniDefinitionApplicationModule>(validate: true);
        });
    }
}
