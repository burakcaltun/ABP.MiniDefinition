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

namespace MiniDefinition.Countries
{
    public class CountryManager : DomainService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryManager(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<Country> CreateAsync(
              string code, 
              string name, 
              DateTime datePassive , 
              int? customsCode, 
    
              YesOrNoEnum? isPassive, 
              ApprovalStatusEnum? approvalStatus
        )
        {

            var country = new Country(
             GuidGenerator.Create(),
               code, 
               name, 
               datePassive , 
               customsCode, 
    
                isPassive, 
                approvalStatus 
             );

            return await _countryRepository.InsertAsync(country);
        }

        public async Task<Country> UpdateAsync(
           Guid id,
          string code, 
          string name, 
          DateTime datePassive , 
          int? customsCode, 

          YesOrNoEnum? isPassive, 
          ApprovalStatusEnum? approvalStatus, 
            [CanBeNull] string concurrencyStamp = null
        )
        {

            var queryable = await _countryRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var country = await AsyncExecuter.FirstOrDefaultAsync(query);

                country.Code=code;
                country.Name=name;
                country.DatePassive=datePassive;         
                 country.CustomsCode=customsCode;
                country.IsPassive=isPassive;  
                country.ApprovalStatus=approvalStatus;  

         country.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _countryRepository.UpdateAsync(country);
        }

    }
}