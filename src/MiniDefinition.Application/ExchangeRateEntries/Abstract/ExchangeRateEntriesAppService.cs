
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using MiniDefinition.Permissions;
using MiniDefinition.ExchangeRateEntries;
using MiniDefinition.Shared;


/// <summary>
    ///  Code Generator ile üretilen abstract siniflarda özellestirme yapilabilmesi için abstract 
    ///  sinifi kalitim alinarak özelleştirme yapilmasi gerekmektedir.
    ///  Code Generator tekrar calistirildiğinda yapilan özellestirmeler kaybolacaktir!!! 

    ///  In order to be able to customize the abstract classes produced with Code Generator,
    ///  it is necessary to inherit the abstract class and customize it.
    ///  Restarting Code Generator, any customizations will be lost!!!
    /// </summary>




namespace MiniDefinition.ExchangeRateEntries.Abstract
{
public abstract class ExchangeRateEntriesAppService :ApplicationService, Interfaces.IExchangeRateEntriesAppService
{
    private readonly IExchangeRateEntryRepository _exchangeRateEntryRepository;
    private readonly ExchangeRateEntryManager _exchangeRateEntryManager;

    public ExchangeRateEntriesAppService(IExchangeRateEntryRepository exchangeRateEntryRepository,ExchangeRateEntryManager exchangeRateEntryManager)
    {
        _exchangeRateEntryRepository = exchangeRateEntryRepository;
        _exchangeRateEntryManager= exchangeRateEntryManager;
    }

    
        //[Authorize(MiniDefinitionPermissions.ExchangeRateEntries.Create)]
    public virtual async Task<ExchangeRateEntryDto> CreateAsync(ExchangeRateEntryCreateDto input)
        {

            var exchangeRateEntry = await _exchangeRateEntryManager.CreateAsync(
                input.Date,
                input.ForexBuying,
                input.ForexSelling,
                input.BanknoteBuying,
                input.BanknoteSelling,
                input.FreeBuyExchangeRate,
                input.FreeSellExchangeRate,
                input.CustomsCode,
                input.CurrencyId
            );
           
            
            return ObjectMapper.Map<ExchangeRateEntry, ExchangeRateEntryDto>(exchangeRateEntry);
        }

        //[Authorize(MiniDefinitionPermissions.ExchangeRateEntries.Create)]
    public virtual async Task<PagedResultDto<ExchangeRateEntryDto>> GetListAsync(GetExchangeRateEntriesInput input)
        {
            var totalCount = await _exchangeRateEntryRepository.GetCountAsync(
            input.FilterText 
            ,input.Date
            ,input.CustomsCode
            ,input.ForexBuying
            ,input.ForexSelling
            ,input.BanknoteBuying
            ,input.BanknoteSelling
            ,input.FreeBuyExchangeRate
            ,input.FreeSellExchangeRate
            
                    
          );
            var items = await _exchangeRateEntryRepository.GetListAsync(
             input.FilterText 
            ,input.Sorting
            ,input.Date
            ,input.ForexBuying
            ,input.ForexSelling
            ,input.BanknoteBuying
            ,input.BanknoteSelling
            ,input.FreeBuyExchangeRate
            ,input.FreeSellExchangeRate
            ,input.CustomsCode
            ,input.MaxResultCount
            ,input.SkipCount      
            ); 
            return new PagedResultDto<ExchangeRateEntryDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List< ExchangeRateEntry>, List<ExchangeRateEntryDto>>(items)
            };
        }


   

    public virtual async Task< ExchangeRateEntryDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<ExchangeRateEntry, ExchangeRateEntryDto>(await _exchangeRateEntryRepository.GetAsync(id));
        }


   
        //[Authorize(MiniDefinitionPermissions.ExchangeRateEntries.Delete)]
    public virtual async Task DeleteAsync(Guid id)
        {
            await _exchangeRateEntryRepository.DeleteAsync(id);
        }

        //[Authorize(MiniDefinitionPermissions.ExchangeRateEntries.Edit)]
     public virtual async Task<ExchangeRateEntryDto> UpdateAsync(Guid id, ExchangeRateEntryUpdateDto input)
         {
    
            var exchangeRateEntry = await _exchangeRateEntryManager.UpdateAsync(
                id,
                input.Date,
                input.ForexBuying,
                input.ForexSelling,
                input.BanknoteBuying,
                input.BanknoteSelling,
                input.FreeBuyExchangeRate,
                input.FreeSellExchangeRate,
                input.CustomsCode,
                input.CurrencyId,
                input.ConcurrencyStamp
            );
           
            
            return ObjectMapper.Map<ExchangeRateEntry, ExchangeRateEntryDto>(exchangeRateEntry);
         }
    



         

        
         

}
}