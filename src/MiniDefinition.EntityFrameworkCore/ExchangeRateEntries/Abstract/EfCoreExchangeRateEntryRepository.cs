using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using MiniDefinition.EntityFrameworkCore;

/// <summary>
   ///  Code Generator ile üretilen abstract siniflarda özellestirme yapilabilmesi için abstract 
   ///  sinifi kalitim alinarak özelleştirme yapilmasi gerekmektedir.
   ///  Code Generator tekrar calistirildiğinda yapilan özellestirmeler kaybolacaktir!!! 

   ///  In order to be able to customize the abstract classes produced with Code Generator,
   ///  it is necessary to inherit the abstract class and customize it.
   ///  Restarting Code Generator, any customizations will be lost!!!
   /// </summary>


namespace MiniDefinition.ExchangeRateEntries.Abstract
{
    public abstract class EfCoreExchangeRateEntryRepository : EfCoreRepository<MiniDefinitionDbContext, ExchangeRateEntry , Guid>, Interfaces.IExchangeRateEntryRepository
    {
        public EfCoreExchangeRateEntryRepository(IDbContextProvider<MiniDefinitionDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        


        public async Task<List<ExchangeRateEntry>> GetListAsync(
             string filterText = null
            ,string sorting = null
            ,DateTime? date= null  
            ,decimal? forexBuying= null 
            ,decimal? forexSelling= null  
            ,decimal? banknoteBuying= null 
            ,decimal? banknoteSelling= null 
            ,decimal? freeBuyExchangeRate= null 
            ,decimal? freeSellExchangeRate= null  
            ,int? customsCode= null  
            
            ,int maxResultCount = int.MaxValue
            ,int skipCount = 0
            ,CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()),filterText
            ,date
            ,forexBuying 
            ,forexSelling 
            ,banknoteBuying 
            ,banknoteSelling  
            ,freeBuyExchangeRate 
            ,freeSellExchangeRate 
            ,customsCode
            );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ExchangeRateEntryConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }



        public async Task<long> GetCountAsync(
         string filterText = null
          ,DateTime? date= null  
          ,int? customsCode= null 
          ,decimal? forexBuying= null   
          ,decimal? forexSelling= null  
          ,decimal? banknoteBuying= null  
          ,decimal? banknoteSelling= null  
          ,decimal? freeBuyExchangeRate= null  
          ,decimal? freeSellExchangeRate= null  
           ,CancellationToken cancellationToken = default
            )
        {
         var query = ApplyFilter((await GetDbSetAsync()), filterText
          ,date        
          ,forexBuying      
          ,forexSelling      
          ,banknoteBuying      
          ,banknoteSelling       
          ,freeBuyExchangeRate      
          ,freeSellExchangeRate       
           ,customsCode
         );
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }


        protected virtual IQueryable<ExchangeRateEntry> ApplyFilter(
            IQueryable<ExchangeRateEntry> query,
            string filterText = null
          ,DateTime? date= null 
          ,decimal? forexBuying= null   
          ,decimal? forexSelling= null  
          ,decimal? banknoteBuying= null  
          ,decimal? banknoteSelling= null  
          ,decimal? freeBuyExchangeRate= null   
          ,decimal? freeSellExchangeRate= null   
          ,int? customsCode= null 
)
        {
            return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
            .WhereIf(date.HasValue, e => e.Date >= date.Value)
            .WhereIf(forexBuying.HasValue, e => e.ForexBuying >= forexBuying.Value)
            .WhereIf(forexSelling.HasValue, e => e.ForexSelling >= forexSelling.Value)
            .WhereIf(banknoteBuying.HasValue, e => e.BanknoteBuying >= banknoteBuying.Value)
            .WhereIf(banknoteSelling.HasValue, e => e.BanknoteSelling >= banknoteSelling.Value)
            .WhereIf(freeBuyExchangeRate.HasValue, e => e.FreeBuyExchangeRate >= freeBuyExchangeRate.Value)
            .WhereIf(freeSellExchangeRate.HasValue, e => e.FreeSellExchangeRate >= freeSellExchangeRate.Value)
            .WhereIf(customsCode.HasValue, e => e.CustomsCode >= customsCode.Value)

         ;
        }
        














        


    }
}
