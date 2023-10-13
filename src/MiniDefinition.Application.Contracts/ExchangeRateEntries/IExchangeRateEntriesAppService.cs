using DevExtreme.AspNet.Data.ResponseModel;
using MiniDefinition.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiniDefinition.ExchangeRateEntries
{
    public interface IExchangeRateEntriesAppService : Interfaces.IExchangeRateEntriesAppService
    {
        Task<LoadResult> BPGetListExhangeRateEntry(DataSourceLoadOptions loadOptions);
        Task<ExchangeRateEntryLookupDto> BPGetExchangeRateEntryByID(Guid id);
        Task<ExchangeRateEntryDto> BPAddExchangeRateEntries(ExchangeRateEntryDto input);
        Task<ExchangeRateEntryDto> BPUpdateExchangeRateEntries(Guid id, ExchangeRateEntryDto input);
        Task BPDeleteExchangeRateEntries(Guid id);
        Task BPCentralBankDataFill();


    }
}
