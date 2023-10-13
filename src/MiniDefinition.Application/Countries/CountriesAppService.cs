using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.Extensions.Localization;
using MiniDefinition.Countries.Passive;
using MiniDefinition.Enums;
using MiniDefinition.Localization;
using MiniDefinition.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;

namespace MiniDefinition.Countries
{
    public class CountriesAppService : Abstract.CountriesAppService, ICountriesAppService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly CountryManager _countryManager;
        private readonly IStringLocalizer<MiniDefinitionResource> _localizer;


        public CountriesAppService(
            ICountryRepository countryRepository,
            CountryManager countryManager,
            IStringLocalizer<MiniDefinitionResource> localizer
            ) : base(countryRepository, countryManager)
        {
            _countryRepository = countryRepository;
            _countryManager = countryManager;
            _localizer = localizer;
        }


        public async Task<LoadResult> BPGetListCountry(string? isActive, DataSourceLoadOptions loadOptions)
        {
            var getCountry = await _countryRepository.GetQueryableAsync();
            getCountry = new IsPassive<Country>(_localizer).GetQueryablewithPassiveAsync(getCountry, isActive);
            getCountry.Select(cnt => new CountryLookupDto
            {
                Id = cnt.Id,
                Code = cnt.Code,
                Name = cnt.Name,
                ApprovalStatus = cnt.ApprovalStatus,
                DatePassive = cnt.DatePassive,
                CustomsCode = cnt.CustomsCode,
                IsPassive = cnt.IsPassive,
                ProcessId = cnt.ProcessId
            });
            return await DataSourceLoader.LoadAsync(getCountry, loadOptions);
        }


        public async Task<CountryLookupDto> BPGetCountryByID(Guid? id)
        {
            var getCountry = await _countryRepository.GetQueryableAsync();
            var country = (from cnt in getCountry
                           where cnt.Id == id
                           select new CountryLookupDto
                           {
                               Id = cnt.Id,
                               Code = cnt.Code,
                               Name = cnt.Name,
                               ApprovalStatus = cnt.ApprovalStatus,
                               DatePassive = cnt.DatePassive,       
                               CustomsCode = cnt.CustomsCode,
                               IsPassive = cnt.IsPassive,
                               ProcessId = cnt.ProcessId

                           }).FirstOrDefault();
            if (country == null)
            {
                throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:SelectRecordNoAccess"], id));
            }

            return country;
        }

        public virtual async Task<List<LookupDto<int>>> BPPassiveStatus()
        {
            var operationType = typeof(YesOrNoEnum).GetEnumValues().Cast<object>().ToDictionary(o => (int)o, x => x.ToString());


            var lookupdata = new List<LookupDto<int>>();
            foreach (var item in operationType)
                lookupdata.Add(new LookupDto<int> { Id = item.Key, DisplayName = _localizer[$"Enum:YesOrNoEnum:{(int)item.Key}"] });
            return lookupdata;
        }

        public CountryDto BPFieldCanNotBeLeftBlank(CountryDto input)
        {
            if (string.IsNullOrEmpty(input.Code))
            {
                throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:FieldCanNotBeLeftBlank"], "Ülke Kodu"));
            }

            if (string.IsNullOrEmpty(input.Name))
            {
                throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:FieldCanNotBeLeftBlank"], "Ülke Adı"));
            }

            return input;
        }


        public async Task<CountryDto> BPAlreadyExist(CountryDto input)
        {

            var getCountry = await _countryRepository.GetQueryableAsync();
            var existingCountry = (from cnt in getCountry
                                   where cnt.Code == input.Code || cnt.Name == input.Name 
                                   select cnt).FirstOrDefault();

            if (existingCountry != null)
            {
                if (existingCountry.Name == input.Name)
                {
                    throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:AlreadyExist"], input.Name));
                }
                if (existingCountry.Code == input.Code)
                {
                    throw new UserFriendlyException(string.Format(_localizer["DEF:Message:Default:AlreadyExist"], input.Code));
                }
            }
            return input;

        }



        public CountryDto BPNullControl(CountryDto input)
        {
            if (input.IsPassive == null)
            {
                throw new UserFriendlyException(_localizer["DEF:Message:Default:NotNullException"]);
            }
            return input;


            //else
            //{
            //    int isActiveValue = (int)input.IsPassive.Value;

            //    if (isActiveValue == (int)YesOrNoEnum.No)
            //    {
            //        input.IsPassive = YesOrNoEnum.No;
            //        return input;
            //    }
            //    else if (isActiveValue == (int)YesOrNoEnum.Yes)
            //    {
            //        input.IsPassive = YesOrNoEnum.Yes;
            //        return input;
            //    }
            //    else
            //    {
            //        throw new UserFriendlyException(_localizer["DEF:Message:Default:InvalidValue"]);
            //    }
            //}
        }

        public async Task BPCountryValidation(CountryDto input)
        {
            // İlk olarak, BPNullControl ile null değerleri ve geçersiz değerleri kontrol edelim.
            BPNullControl(input);

            // Ardından, BPAlreadyExist ile ülkenin zaten varlığını kontrol edelim.
            await BPAlreadyExist(input);

            // Son olarak, BPFieldCanNotBeLeftBlank ile gerekli alanların boş olup olmadığını kontrol edelim.
            BPFieldCanNotBeLeftBlank(input);

        }

        public async Task<CountryDto> BPAddCountry(CountryDto input)
        {
            await BPCountryValidation(input);

            var country = await _countryManager.CreateAsync(
                
                input.Code,
                input.Name,
                input.DatePassive,
                input.CustomsCode,
                input.IsPassive,
                input.ApprovalStatus
                );

            return ObjectMapper.Map<Country, CountryDto>(country);
        }

        public async Task<CountryDto> BPUpdateCountry(Guid id, CountryDto input)
        {
            await BPCountryValidation(input);

            var cnt = await _countryManager.UpdateAsync(
                id,
                input.Code,
                input.Name,
                input.DatePassive,
                input.CustomsCode,
                input.IsPassive,
                input.ApprovalStatus
                );

            return ObjectMapper.Map<Country, CountryDto>(cnt);
        }


        public async Task BPDeleteCountry(Guid id)
        {
           
            await _countryRepository.DeleteAsync(id);

        }






    }
}
