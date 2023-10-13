

using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using MiniDefinition.Shared;


namespace MiniDefinition.ExchangeRateEntries.Interfaces

{
    public interface IExchangeRateEntriesAppService: IApplicationService
    {
        

        Task<PagedResultDto< ExchangeRateEntryDto >> GetListAsync(GetExchangeRateEntriesInput input);

        Task<ExchangeRateEntryDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ExchangeRateEntryDto> CreateAsync(ExchangeRateEntryCreateDto input);

        Task<ExchangeRateEntryDto> UpdateAsync(Guid id, ExchangeRateEntryUpdateDto input);

        
    }
}


