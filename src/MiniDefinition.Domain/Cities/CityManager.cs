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

namespace MiniDefinition.Cities
{
    public class CityManager : DomainService
    {
        private readonly ICityRepository _cityRepository;

        public CityManager(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<City> CreateAsync(
              string cityCode, 
              string cityName, 
              DateTime datePassive , 
              YesOrNoEnum? isPassive, 
              ApprovalStatusEnum? approvalStatus, 
              Guid? countryId
        )
        {

            var city = new City(
             GuidGenerator.Create(),
               cityCode, 
               cityName, 
               datePassive , 
                isPassive, 
                approvalStatus, 
               countryId
             );

            return await _cityRepository.InsertAsync(city);
        }

        public async Task<City> UpdateAsync(
           Guid id,
          string cityCode, 
          string cityName, 
          DateTime datePassive , 
          YesOrNoEnum? isPassive, 
          ApprovalStatusEnum? approvalStatus, 
          Guid? countryId,
            [CanBeNull] string concurrencyStamp = null
        )
        {

            var queryable = await _cityRepository.GetQueryableAsync();
            var query = queryable.Where(x => x.Id == id);

            var city = await AsyncExecuter.FirstOrDefaultAsync(query);

                city.CityCode=cityCode;
                city.CityName=cityName;
                city.DatePassive=datePassive;         
                city.IsPassive=isPassive;  
                city.ApprovalStatus=approvalStatus;  
                city.CountryId=countryId;

         city.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _cityRepository.UpdateAsync(city);
        }

    }
}