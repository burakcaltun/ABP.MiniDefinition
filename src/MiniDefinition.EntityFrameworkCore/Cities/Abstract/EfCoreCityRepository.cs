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


namespace MiniDefinition.Cities.Abstract
{
    public abstract class EfCoreCityRepository : EfCoreRepository<MiniDefinitionDbContext, City , Guid>, Interfaces.ICityRepository
    {
        public EfCoreCityRepository(IDbContextProvider<MiniDefinitionDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        


        public async Task<List<City>> GetListAsync(
             string filterText = null
            ,string sorting = null
            ,string cityCode= null 
            ,string cityName= null 
            ,DateTime? datePassive= null  
            ,YesOrNoEnum? isPassive= null 
          
            ,ApprovalStatusEnum? approvalStatus= null 
          
            
            ,int maxResultCount = int.MaxValue
            ,int skipCount = 0
            ,CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()),filterText,
               cityCode
,
               cityName
             ,datePassive
            ,isPassive 
          
            ,approvalStatus 
          
            );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? CityConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }



        public async Task<long> GetCountAsync(
         string filterText = null
          ,string cityCode= null 
          ,string cityName= null 
          ,DateTime? datePassive= null  
           ,YesOrNoEnum? isPassive= null 
          
           ,ApprovalStatusEnum? approvalStatus= null 
          
           ,CancellationToken cancellationToken = default
            )
        {
         var query = ApplyFilter((await GetDbSetAsync()), filterText,cityCode
,cityName
           ,datePassive    
           ,isPassive   
           ,approvalStatus   
         );
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }


        protected virtual IQueryable<City> ApplyFilter(
            IQueryable<City> query,
            string filterText = null
          ,string cityCode= null  
          ,string cityName= null  
          ,DateTime? datePassive= null 
          ,YesOrNoEnum? isPassive= null 
          
          ,ApprovalStatusEnum? approvalStatus= null 
          
)
        {
            return query
            .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),e => e.CityCode.Contains(filterText)) 
            .WhereIf(!string.IsNullOrWhiteSpace(filterText),e => e.CityName.Contains(filterText)) 
            .WhereIf(datePassive.HasValue, e => e.DatePassive >= datePassive.Value)

            .WhereIf(!string.IsNullOrWhiteSpace(cityCode),e => e.CityCode.Contains(cityCode)) 
            .WhereIf(!string.IsNullOrWhiteSpace(cityName),e => e.CityName.Contains(cityName)) 
         ;
        }
        














        


    }
}
