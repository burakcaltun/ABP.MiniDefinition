



using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MiniDefinition.ExchangeRateEntries;


/// <summary>
    ///  Code Generator ile üretilen abstract sınıflarda özellestirme yapılabilmesi için abstract 
    ///  sinifi kalitim alınarak özelleştirme yapilmasi gerekmektedir.
    ///  Code Generator tekrar calistirildiğinda yapılan özellestirmeler kaybolacaktir!!! 

    ///  In order to be able to customize the abstract classes produced with Code Generator,
    ///  it is necessary to inherit the abstract class and customize it.
    ///  Restarting Code Generator, any customizations will be lost!!!
    /// </summary>


namespace  MiniDefinition.Controllers.ExchangeRateEntries.Abstract
{
    
    [Route("api/exchange-rate-entries")]
    
    public abstract class ExchangeRateEntriesController : AbpController, MiniDefinition.ExchangeRateEntries.Interfaces.IExchangeRateEntriesAppService
    {
        private readonly MiniDefinition.ExchangeRateEntries.Interfaces.IExchangeRateEntriesAppService _exchangeRateEntriesAppService;

        

        public ExchangeRateEntriesController(MiniDefinition.ExchangeRateEntries.Interfaces.IExchangeRateEntriesAppService exchangeRateEntriesAppService)
       {
        _exchangeRateEntriesAppService = exchangeRateEntriesAppService;
       }

        [HttpPost]
        
        public virtual Task<ExchangeRateEntryDto> CreateAsync( ExchangeRateEntryCreateDto  input)
        {
            
                return _exchangeRateEntriesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ExchangeRateEntryDto> UpdateAsync(Guid id,  ExchangeRateEntryUpdateDto  input)
        {
            return _exchangeRateEntriesAppService.UpdateAsync(id,input);
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ExchangeRateEntryDto>> GetListAsync(GetExchangeRateEntriesInput input)
        {
            return _exchangeRateEntriesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ExchangeRateEntryDto> GetAsync( Guid id)
        {
            return _exchangeRateEntriesAppService.GetAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync( Guid id)
        {
            return _exchangeRateEntriesAppService.DeleteAsync(id);
        }
    }
}
