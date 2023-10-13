using MiniDefinition.Localization;
using Volo.Abp.Application.Services;

namespace MiniDefinition;

public abstract class MiniDefinitionAppService : ApplicationService
{
    protected MiniDefinitionAppService()
    {
        LocalizationResource = typeof(MiniDefinitionResource);
        ObjectMapperContext = typeof(MiniDefinitionApplicationModule);
    }
}
