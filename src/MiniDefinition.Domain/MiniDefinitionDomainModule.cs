using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MiniDefinition;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(MiniDefinitionDomainSharedModule)
)]
public class MiniDefinitionDomainModule : AbpModule
{

}
