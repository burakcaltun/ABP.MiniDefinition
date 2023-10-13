using Localization.Resources.AbpUi;
using MiniDefinition.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace MiniDefinition;

[DependsOn(
typeof(MiniDefinitionApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class MiniDefinitionHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MiniDefinitionHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MiniDefinitionResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
