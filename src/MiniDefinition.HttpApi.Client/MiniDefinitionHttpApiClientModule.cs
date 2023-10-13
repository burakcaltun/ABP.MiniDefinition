using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MiniDefinition;

[DependsOn(
    typeof(MiniDefinitionApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class MiniDefinitionHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(MiniDefinitionApplicationContractsModule).Assembly,
            MiniDefinitionRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MiniDefinitionHttpApiClientModule>();
        });

    }
}
