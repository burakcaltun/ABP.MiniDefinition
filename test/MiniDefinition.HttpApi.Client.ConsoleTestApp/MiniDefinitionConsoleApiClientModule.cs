using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace MiniDefinition;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MiniDefinitionHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class MiniDefinitionConsoleApiClientModule : AbpModule
{

}
