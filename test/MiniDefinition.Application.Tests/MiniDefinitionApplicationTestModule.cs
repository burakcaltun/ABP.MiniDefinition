using Volo.Abp.Modularity;

namespace MiniDefinition;

[DependsOn(
    typeof(MiniDefinitionApplicationModule),
    typeof(MiniDefinitionDomainTestModule)
    )]
public class MiniDefinitionApplicationTestModule : AbpModule
{

}
