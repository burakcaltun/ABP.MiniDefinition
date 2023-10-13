using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MiniDefinition.ExchangeRateEntries.Interfaces
{
    


    public interface IExchangeRateEntryRepository : IRepository<ExchangeRateEntry, Guid>
{

  

  
      Task<List< ExchangeRateEntry>> GetListAsync(
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
         ,CancellationToken cancellationToken = default      
       );

       Task<long> GetCountAsync(
        string filterText = null,
          DateTime? date= null , 
          int? customsCode= null , 
          decimal? forexBuying= null , 
          decimal? forexSelling= null , 
          decimal? banknoteBuying= null , 
          decimal? banknoteSelling= null , 
          decimal? freeBuyExchangeRate= null , 
          decimal? freeSellExchangeRate= null , 
        CancellationToken cancellationToken = default);

        

    }
}
