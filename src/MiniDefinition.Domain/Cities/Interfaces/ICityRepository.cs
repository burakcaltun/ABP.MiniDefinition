using MiniDefinition.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MiniDefinition.Cities.Interfaces
{
    


    public interface ICityRepository : IRepository<City, Guid>
{

  

  
      Task<List< City>> GetListAsync(
         string filterText = null
         ,string sorting = null
         ,string cityCode= null 
         ,string cityName= null 
         ,DateTime? datePassive= null  
         ,YesOrNoEnum? isPassive= null 
       
         ,ApprovalStatusEnum? approvalStatus= null 
       
         ,int maxResultCount = int.MaxValue
         ,int skipCount = 0
         ,CancellationToken cancellationToken = default      
       );

       Task<long> GetCountAsync(
        string filterText = null,
          string cityCode= null , 
          string cityName= null , 
          DateTime? datePassive= null , 
          YesOrNoEnum? isPassive= null , 
          
          ApprovalStatusEnum? approvalStatus= null , 
          
        CancellationToken cancellationToken = default);

        

    }
}
