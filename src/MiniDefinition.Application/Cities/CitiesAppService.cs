using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.Extensions.Localization;
using MiniDefinition.Countries;
using MiniDefinition.Countries.Passive;
using MiniDefinition.Enums;
using MiniDefinition.Localization;
using MiniDefinition.Shared;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace MiniDefinition.Cities
{
    public class CitiesAppService : Abstract.CitiesAppService, ICitiesAppService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly CityManager _cityManager;
        private readonly IStringLocalizer<MiniDefinitionResource> _localizer;








        public CitiesAppService(
            ICityRepository cityRepository,
            ICountryRepository countryRepository,
            CityManager cityManager,
            IStringLocalizer<MiniDefinitionResource> localizer) 
            : base(cityRepository, cityManager)
        {
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _cityManager = cityManager;
            _localizer = localizer;
            
        }


        public async Task<LoadResult> BPGetListCity(string? isActive, DataSourceLoadOptions loadOptions)
        {
            var getCity = await _cityRepository.GetQueryableAsync();
            getCity = new IsPassive<City>(_localizer).GetQueryablewithPassiveAsync(getCity, isActive);
            getCity.Select(cty => new CityLookupDto
            {
                Id = cty.Id,
                CityCode = cty.CityCode,
                CityName = cty.CityName,
                IsPassive = cty.IsPassive,
                DatePassive = cty.DatePassive,
                ApprovalStatus = cty.ApprovalStatus,
                ProcessId = cty.ProcessId,
                CountryId = cty.CountryId,
            });
            return await DataSourceLoader.LoadAsync(getCity, loadOptions);
        }


        public async Task<CityLookupDto> BPGetCityByID(Guid? id)
        {
            var getCity = await _cityRepository.GetQueryableAsync();
            var city = (from cty in getCity
                        where cty.Id == id
                           select new CityLookupDto
                           {
                               Id = cty.Id,
                               CityCode = cty.CityCode,
                               CityName = cty.CityName,
                               IsPassive = cty.IsPassive,
                               DatePassive = cty.DatePassive,
                               ApprovalStatus = cty.ApprovalStatus,
                               ProcessId = cty.ProcessId,
                               CountryId = cty.CountryId,

                           }).FirstOrDefault();
            if (city == null)
            {
                throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:SelectRecordNoAccess"], id));
            }

            return city;
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input)
        {
            var totalCount = await _countryRepository.GetCountAsync();

            var countries = await _countryRepository.GetQueryableAsync();
            var countriesList = await countries
                .OrderBy(c => c.Name)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(c => new LookupDto<Guid>
                {
                    Id = c.Id,
                    Code = c.Code,
                    DisplayName = c.Name
                })
                .ToListAsync();

            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = countriesList
            };
        }




        public virtual async Task<List<LookupDto<int>>> BPPassiveStatus()
        {
            var operationType = typeof(YesOrNoEnum).GetEnumValues().Cast<object>().ToDictionary(o => (int)o, x => x.ToString());

            var lookupdata = new List<LookupDto<int>>();
            foreach(var item in operationType)
            {
                lookupdata.Add(new LookupDto<int> { Id = item.Key, DisplayName = _localizer[$"Enum:YesOrNoEnum:{(int)item.Key}"] });
            }
            return lookupdata;

        } 

        public CityDto BPFieldCanNotBeLeftBlank(CityDto input)
        {
            if (string.IsNullOrEmpty(input.CityCode))
            {
                throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:FieldCanNotBeLeftBlank"], "Şehir Kodu"));
            }

            if (string.IsNullOrEmpty(input.CityName))
            {
                throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:FieldCanNotBeLeftBlank"], "Şehir Adı"));
            }

            return input;
        }

        public async Task<CityDto> BPAlreadyExist(CityDto input)
        {
            var getCity = await _cityRepository.GetQueryableAsync();
            var existingCity = (from cty in getCity
                                where cty.CityCode == input.CityCode ||  cty.CityName == input.CityName 
                                select cty).FirstOrDefault();

            if (existingCity != null)
            {
                if (existingCity.CityCode == input.CityCode)
                {
                    throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:AlreadyExist"], input.CityCode));
                }
                if(existingCity.CityName == input.CityName)
                {
                    throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:AlreadyExist"], input.CityName));
                }
            }
            return input;
        }

        public CityDto BPNullControl(CityDto input)
        {
            if(input.IsPassive == null)
            {
                throw new UserFriendlyException(_localizer["DEF:Message:Default:NotNullException"]);
            }
            return input;
        }

        public async Task BPCityValidation(CityDto input)
        {
            // İlk olarak, BPNullControl ile null değerleri ve geçersiz değerleri kontrol edelim.
            BPNullControl(input);

            // Ardından, BPAlreadyExist ile ülkenin zaten varlığını kontrol edelim.
            await BPAlreadyExist(input);

            // Son olarak, BPFieldCanNotBeLeftBlank ile gerekli alanların boş olup olmadığını kontrol edelim.
            BPFieldCanNotBeLeftBlank(input);

        }

        public async Task<CityDto> BPAddCity(CityDto input)
        {
            await BPCityValidation(input);

            var city = await _cityManager.CreateAsync(
                input.CityCode,
                input.CityName,
                input.DatePassive,
                input.IsPassive,
                input.ApprovalStatus,
                input.CountryId);

            return ObjectMapper.Map<City, CityDto>(city);
        }


        public async Task<CityDto> BPUpdateCity(Guid id, CityDto input)
        {
            await BPCityValidation(input);

            var city = await _cityManager.UpdateAsync(
                id,
                input.CityCode,
                input.CityName,
                input.DatePassive,
                input.IsPassive,
                input.ApprovalStatus,
                input.CountryId);

            return ObjectMapper.Map<City, CityDto>(city);
        }

        public async Task BPDeleteCity(Guid id)
        {
            await _cityRepository.DeleteAsync(id);
        }

    }
}
