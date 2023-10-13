



using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiniDefinition.Currencies;


/// <summary>
    ///  Code Generator ile üretilen abstract sınıflarda özellestirme yapılabilmesi için abstract 
    ///  sinifi kalitim alınarak özelleştirme yapilmasi gerekmektedir.
    ///  Code Generator tekrar calistirildiğinda yapılan özellestirmeler kaybolacaktir!!! 

    ///  In order to be able to customize the abstract classes produced with Code Generator,
    ///  it is necessary to inherit the abstract class and customize it.
    ///  Restarting Code Generator, any customizations will be lost!!!
    /// </summary>


namespace  MiniDefinition.Controllers.Currencies
{
    
    [Route("api/currencies")]
    
    public class CurrenciesController : AbpController, ICurrenciesAppService
    {
        private readonly ICurrenciesAppService _currenciesAppService;

        

        public CurrenciesController(ICurrenciesAppService currenciesAppService)
       {
        _currenciesAppService = currenciesAppService;
       }

        [HttpPost]
        public virtual Task<CurrencyDto> CreateAsync( CurrencyCreateDto  input)
        {
            
                return _currenciesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<CurrencyDto> UpdateAsync(Guid id,  CurrencyUpdateDto  input)
        {
            return _currenciesAppService.UpdateAsync(id,input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input)
        {
            return _currenciesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<CurrencyDto> GetAsync( Guid id)
        {
            return _currenciesAppService.GetAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync( Guid id)
        {
            return _currenciesAppService.DeleteAsync(id);
        }
    }
}
