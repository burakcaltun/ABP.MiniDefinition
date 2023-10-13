using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MiniDefinition.Countries;
using MiniDefinition.Countries.Passive;
using MiniDefinition.Localization;
using MiniDefinition.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDefinition.Controllers.Countries
{
    public class CountriesController : Abstract.CountriesController, ICountriesAppService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ICountriesAppService _countriesAppService;
        private readonly IStringLocalizer<MiniDefinitionResource> _localizer;


        public CountriesController(ICountriesAppService countriesAppService, ICountryRepository countryRepository, IStringLocalizer<MiniDefinitionResource> localizer) : base(countriesAppService)
        {
            _countriesAppService = countriesAppService;
            _countryRepository = countryRepository;
            _localizer = localizer;

        }

        //[HttpGet]
        //[Route("bp-get-list-payments/{isActive}")]
        //public async Task<LoadResult> BPGetListPayments(Guid userId, SalePurchaseEnum salePurchaseEnum, string? isActive, DataSourceLoadOptions loadOptions, Guid? language, Guid? secondlanguage)
        //{
        //    return await _countriesAppService.BPGetListPayments(userId, salePurchaseEnum, isActive, loadOptions, language, secondlanguage);
        //}

        [HttpGet]
        [Route("bp-get-list-country/{isActive}")]
        public async Task<LoadResult> BPGetListCountry(string? isActive, DataSourceLoadOptions loadOptions)
        {
            return await _countriesAppService.BPGetListCountry(isActive, loadOptions);
        }


        [HttpGet]
        [Route("bp-get-country-by-id/{id}")]
        public async Task<CountryLookupDto> BPGetCountryByID(Guid? id)
        {
            return await _countriesAppService.BPGetCountryByID(id);
        }

        [HttpGet]
        [Route("bp-passive-status")]
        public virtual async Task<List<LookupDto<int>>> BPPassiveStatus()
        {
            return await _countriesAppService.BPPassiveStatus();
        }

        [HttpPost]
        [Route("bp-add-country")]
        public async Task<CountryDto> BPAddCountry(CountryDto input)
        {
            return await _countriesAppService.BPAddCountry(input);
        }

        [HttpPut]
        [Route("bp-update-country/{id}")]
        public async Task<CountryDto> BPUpdateCountry(Guid id, CountryDto input)
        {
            return await _countriesAppService.BPUpdateCountry(id, input);
        }

        [HttpDelete]
        [Route("bp-delete-country/{id}")]
        public async Task BPDeleteCountry(Guid id)
        {
            await _countriesAppService.BPDeleteCountry(id);
            // await _exchangeRateEntriesAppService.BPDeleteExchangeRateEntries(id);
        }

    }
}
