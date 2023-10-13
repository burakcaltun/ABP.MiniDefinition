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
using MiniDefinition.Enums;

/// <summary>
///  Code Generator ile üretilen abstract siniflarda özellestirme yapilabilmesi için abstract 
///  sinifi kalitim alinarak özelleştirme yapilmasi gerekmektedir.
///  Code Generator tekrar calistirildiğinda yapilan özellestirmeler kaybolacaktir!!! 

///  In order to be able to customize the abstract classes produced with Code Generator,
///  it is necessary to inherit the abstract class and customize it.
///  Restarting Code Generator, any customizations will be lost!!!
/// </summary>


namespace MiniDefinition.Currencies
{
    public class EfCoreCurrencyRepository : EfCoreRepository<MiniDefinitionDbContext, Currency, Guid>, ICurrencyRepository
    {
        public EfCoreCurrencyRepository(IDbContextProvider<MiniDefinitionDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        


        public async Task<List<Currency>> GetListAsync(
             string? filterText = null
            ,string sorting = null
            ,string? code= null 
            ,string? name= null 
            ,DateTime? datePassive= null 
            ,int? number= null 
            ,YesOrNoEnum? isPassive= null 
          
            ,ApprovalStatusEnum? approvalStatus= null 
          
            
            ,int maxResultCount = int.MaxValue
            ,int skipCount = 0
            ,CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()),filterText,
               code
,
               name
             ,datePassive
            ,number
            ,isPassive 
          
            ,approvalStatus 
          
            );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CurrencyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }



        public async Task<long> GetCountAsync(
         string? filterText = null
          ,string code= null 
          ,string name= null 
          ,DateTime? datePassive= null  
          ,int? number= null 
           ,YesOrNoEnum? isPassive= null 
          
           ,ApprovalStatusEnum? approvalStatus= null 
          
           ,CancellationToken cancellationToken = default
            )
        {
         var query = ApplyFilter((await GetDbSetAsync()), filterText,code
,name
           ,datePassive    
           ,number
           ,isPassive   
           ,approvalStatus   
         );
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }


        protected virtual IQueryable<Currency> ApplyFilter(
            IQueryable<Currency> query,
            string? filterText = null
          ,string? code= null  
          ,string? name= null  
          ,DateTime? datePassive= null 
          ,int? number= null 
          ,YesOrNoEnum? isPassive= null 
          
          ,ApprovalStatusEnum? approvalStatus= null 
          
)
        {
            return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),e => e.Code.Contains(filterText)) 
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),e => e.Name.Contains(filterText)) 
            .WhereIf(datePassive.HasValue, e => e.DatePassive >= datePassive.Value)
            .WhereIf(number.HasValue, e => e.Number >= number.Value)


            .WhereIf(!string.IsNullOrWhiteSpace(code),e => e.Code.Contains(code)) 
            .WhereIf(!string.IsNullOrWhiteSpace(name),e => e.Name.Contains(name)) 
         ;
        }
        














        


    }
}
