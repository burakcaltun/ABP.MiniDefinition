



using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiniDefinition.Cities;


/// <summary>
///  Code Generator ile üretilen abstract sınıflarda özellestirme yapılabilmesi için abstract 
///  sinifi kalitim alınarak özelleştirme yapilmasi gerekmektedir.
///  Code Generator tekrar calistirildiğinda yapılan özellestirmeler kaybolacaktir!!! 

///  In order to be able to customize the abstract classes produced with Code Generator,
///  it is necessary to inherit the abstract class and customize it.
///  Restarting Code Generator, any customizations will be lost!!!
/// </summary>


namespace  MiniDefinition.Controllers.Cities.Abstract
{
    
    [Route("api/cities")]
    
    public abstract class CitiesController : AbpController, MiniDefinition.Cities.Interfaces.ICitiesAppService
    {
        private readonly MiniDefinition.Cities.Interfaces.ICitiesAppService _citiesAppService;

        

        public CitiesController(MiniDefinition.Cities.Interfaces.ICitiesAppService citiesAppService)
       {
        _citiesAppService = citiesAppService;
       }

        [HttpPost]
        
        public virtual Task<CityDto> CreateAsync( CityCreateDto  input)
        {
            
                return _citiesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CityDto> UpdateAsync(Guid id,  CityUpdateDto  input)
        {
            return _citiesAppService.UpdateAsync(id,input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CityDto>> GetListAsync(GetCitiesInput input)
        {
            return _citiesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CityDto> GetAsync( Guid id)
        {
            return _citiesAppService.GetAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync( Guid id)
        {
            return _citiesAppService.DeleteAsync(id);
        }
    }
}
