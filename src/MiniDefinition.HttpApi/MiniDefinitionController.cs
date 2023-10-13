using MiniDefinition.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MiniDefinition;

public abstract class MiniDefinitionController : AbpControllerBase
{
    protected MiniDefinitionController()
    {
        LocalizationResource = typeof(MiniDefinitionResource);
    }
}
