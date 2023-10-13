using MiniDefinition.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MiniDefinition.Currencies
{
    


    public interface ICurrencyRepository : IRepository<Currency, Guid>
{

  

  
      Task<List< Currency>> GetListAsync(
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
         ,CancellationToken cancellationToken = default      
       );

       Task<long> GetCountAsync(
        string? filterText = null,
          string code= null , 
          string name= null , 
          DateTime? datePassive= null , 
          int? number= null , 
          YesOrNoEnum? isPassive= null , 
          
          ApprovalStatusEnum? approvalStatus= null , 
          
        CancellationToken cancellationToken = default);

        

    }
}
