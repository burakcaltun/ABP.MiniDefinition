using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using MiniDefinition.Enums;

namespace MiniDefinition.Currencies
{
    public class CurrencyManager : DomainService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyManager(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task<Currency> CreateAsync(
              string code, 
              string name, 
              DateTime datePassive , 
              int? number, 
    
              YesOrNoEnum? isPassive, 
              ApprovalStatusEnum? approvalStatus
        )
        {

            var currency = new Currency(
             GuidGenerator.Create(),
               code, 
               name, 
               datePassive , 
               number, 
    
                isPassive, 
                approvalStatus
             );

            return await _currencyRepository.InsertAsync(currency);
        }

        public async Task<Currency> UpdateAsync(
           Guid id,
          string code, 
          string name, 
          DateTime datePassive , 
          int? number, 

          YesOrNoEnum? isPassive, 
          ApprovalStatusEnum? approvalStatus, 
            [CanBeNull] string concurrencyStamp = null
        )
        {

            var queryable = await _currencyRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var currency = await AsyncExecuter.FirstOrDefaultAsync(query);

                currency.Code=code;
                currency.Name=name;
                currency.DatePassive=datePassive;         
                 currency.Number=number;
                currency.IsPassive=isPassive;  
                currency.ApprovalStatus=approvalStatus;  

         currency.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _currencyRepository.UpdateAsync(currency);
        }

    }
}