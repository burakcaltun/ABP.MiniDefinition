using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace MiniDefinition;

[DependsOn(
    typeof(MiniDefinitionDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class MiniDefinitionApplicationContractsModule : AbpModule
{

}
