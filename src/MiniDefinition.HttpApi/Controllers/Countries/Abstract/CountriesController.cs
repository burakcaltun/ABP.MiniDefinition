



using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiniDefinition.Countries;


/// <summary>
    ///  Code Generator ile üretilen abstract sınıflarda özellestirme yapılabilmesi için abstract 
    ///  sinifi kalitim alınarak özelleştirme yapilmasi gerekmektedir.
    ///  Code Generator tekrar calistirildiğinda yapılan özellestirmeler kaybolacaktir!!! 

    ///  In order to be able to customize the abstract classes produced with Code Generator,
    ///  it is necessary to inherit the abstract class and customize it.
    ///  Restarting Code Generator, any customizations will be lost!!!
    /// </summary>


namespace  MiniDefinition.Controllers.Countries.Abstract
{
    
    [Route("api/countries")]
    
    public abstract class CountriesController : AbpController, MiniDefinition.Countries.Interfaces.ICountriesAppService
    {
        private readonly MiniDefinition.Countries.Interfaces.ICountriesAppService _countriesAppService;

        

        public CountriesController(MiniDefinition.Countries.Interfaces.ICountriesAppService countriesAppService)
       {
        _countriesAppService = countriesAppService;
       }

        [HttpPost]
        
        public virtual Task<CountryDto> CreateAsync( CountryCreateDto  input)
        {
            
                return _countriesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CountryDto> UpdateAsync(Guid id,  CountryUpdateDto  input)
        {
            return _countriesAppService.UpdateAsync(id,input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CountryDto>> GetListAsync(GetCountriesInput input)
        {
            return _countriesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CountryDto> GetAsync( Guid id)
        {
            return _countriesAppService.GetAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync( Guid id)
        {
            return _countriesAppService.DeleteAsync(id);
        }
    }
}
