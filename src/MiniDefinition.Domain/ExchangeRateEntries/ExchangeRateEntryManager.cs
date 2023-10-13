using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace MiniDefinition.ExchangeRateEntries
{
    public class ExchangeRateEntryManager : DomainService
    {
        private readonly IExchangeRateEntryRepository _exchangeRateEntryRepository;

        public ExchangeRateEntryManager(IExchangeRateEntryRepository exchangeRateEntryRepository)
        {
            _exchangeRateEntryRepository = exchangeRateEntryRepository;
        }

        public async Task<ExchangeRateEntry> CreateAsync(
              DateTime date , 
              decimal? forexBuying, 
              decimal? forexSelling, 
              decimal? banknoteBuying, 
              decimal? banknoteSelling, 
              decimal? freeBuyExchangeRate, 
              decimal? freeSellExchangeRate, 
              int? customsCode, 
    
              Guid? currencyId
        )
        {

            var exchangeRateEntry = new ExchangeRateEntry(
             GuidGenerator.Create(),
               date , 
               forexBuying, 
               forexSelling, 
               banknoteBuying, 
               banknoteSelling, 
               freeBuyExchangeRate, 
               freeSellExchangeRate, 
               customsCode, 
    
               currencyId
             );

            return await _exchangeRateEntryRepository.InsertAsync(exchangeRateEntry);
        }

        public async Task<ExchangeRateEntry> UpdateAsync(
           Guid id,
          DateTime date , 
          decimal? forexBuying, 
          decimal? forexSelling, 
          decimal? banknoteBuying, 
          decimal? banknoteSelling, 
          decimal? freeBuyExchangeRate, 
          decimal? freeSellExchangeRate, 
          int? customsCode, 

          Guid? currencyId,
            [CanBeNull] string concurrencyStamp = null
        )
        {

            var queryable = await _exchangeRateEntryRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var exchangeRateEntry = await AsyncExecuter.FirstOrDefaultAsync(query);

                exchangeRateEntry.Date=date;         
                exchangeRateEntry.ForexBuying=forexBuying;        
                exchangeRateEntry.ForexSelling=forexSelling;        
                exchangeRateEntry.BanknoteBuying=banknoteBuying;        
                exchangeRateEntry.BanknoteSelling=banknoteSelling;        
                exchangeRateEntry.FreeBuyExchangeRate=freeBuyExchangeRate;        
                exchangeRateEntry.FreeSellExchangeRate=freeSellExchangeRate;        
                 exchangeRateEntry.CustomsCode=customsCode;
                exchangeRateEntry.CurrencyId=currencyId;

         exchangeRateEntry.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _exchangeRateEntryRepository.UpdateAsync(exchangeRateEntry);
        }

    }
}