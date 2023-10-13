using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using MiniDefinition.Shared;


namespace MiniDefinition.Currencies

{
    public interface ICurrenciesAppService: IApplicationService
    {
        

        Task<PagedResultDto< CurrencyDto >> GetListAsync(GetCurrenciesInput input);

        Task<CurrencyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CurrencyDto> CreateAsync(CurrencyCreateDto input);

        Task<CurrencyDto> UpdateAsync(Guid id, CurrencyUpdateDto input);

        
    }
}


