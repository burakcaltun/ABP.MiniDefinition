using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using MiniDefinition.Cities;
using MiniDefinition.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace MiniDefinition.Controllers.Cities
{
    public class CitiesController : Abstract.CitiesController, ICitiesAppService
    {
        private readonly ICitiesAppService _citiesAppService;
        public CitiesController(ICitiesAppService citiesAppService) : base(citiesAppService)
        {
            _citiesAppService = citiesAppService;
        }

        //[HttpGet]
        //[Route("bp-get-list-payments/{isActive}")]
        //public async Task<LoadResult> BPGetListPayments(Guid userId, SalePurchaseEnum salePurchaseEnum, string? isActive, DataSourceLoadOptions loadOptions, Guid? language, Guid? secondlanguage)
        //{
        //    return await _countriesAppService.BPGetListPayments(userId, salePurchaseEnum, isActive, loadOptions, language, secondlanguage);
        //}

        [HttpGet]
        [Route("bp-get-list-city/{isActive}")]
        public async Task<LoadResult> BPGetListCity(string? isActive, DataSourceLoadOptions loadOptions)
        {
            return await _citiesAppService.BPGetListCity(isActive, loadOptions);
        }

        [HttpGet]
        [Route("bp-get-city-by-id/{id}")]
        public async Task<CityLookupDto> BPGetCityByID(Guid? id)
        {
            return await _citiesAppService.BPGetCityByID(id);
        }

        [HttpGet]
        [Route("get-country-lookup-async")]
        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetCountryLookupAsync(LookupRequestDto input)
        {
            return await _citiesAppService.GetCountryLookupAsync(input);
        }

        [HttpGet]
        [Route("get-passive-status")]
        public virtual async Task<List<LookupDto<int>>> BPPassiveStatus()
        {
            return await _citiesAppService.BPPassiveStatus();
        }


        [HttpPost]
        [Route("bp-add-city")]
        public async Task<CityDto> BPAddCity(CityDto input)
        {
            return await _citiesAppService.BPAddCity(input);
        }

        [HttpPut]
        [Route("bp-update-city/{id}")]
        public async Task<CityDto> BPUpdateCity(Guid id, CityDto input)
        {
            return await _citiesAppService.BPUpdateCity(id, input);
        }

        [HttpDelete]
        [Route("bp-delete-city/{id}")]
        public async Task BPDeleteCity(Guid id)
        {
            await _citiesAppService.BPDeleteCity(id);
        }
    }
}
