
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using MiniDefinition.Permissions;



/// <summary>
    ///  Code Generator ile üretilen abstract siniflarda özellestirme yapilabilmesi için abstract 
    ///  sinifi kalitim alinarak özelleştirme yapilmasi gerekmektedir.
    ///  Code Generator tekrar calistirildiğinda yapilan özellestirmeler kaybolacaktir!!! 

    ///  In order to be able to customize the abstract classes produced with Code Generator,
    ///  it is necessary to inherit the abstract class and customize it.
    ///  Restarting Code Generator, any customizations will be lost!!!
    /// </summary>




namespace MiniDefinition.Currencies
{
    public class CurrenciesAppService : ApplicationService, ICurrenciesAppService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly CurrencyManager _currencyManager;

        public CurrenciesAppService(ICurrencyRepository currencyRepository, CurrencyManager currencyManager)
        {
            _currencyRepository = currencyRepository;
            _currencyManager = currencyManager;
        }


        //[Authorize(MiniDefinitionPermissions.Currencies.Create)]
        public virtual async Task<CurrencyDto> CreateAsync(CurrencyCreateDto input)
        {

            var currency = await _currencyManager.CreateAsync(
                input.Code,
                input.Name,
                input.DatePassive,
                input.Number,
                input.IsPassive,
                input.ApprovalStatus
            );


            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }

        //[Authorize(MiniDefinitionPermissions.Currencies.Create)]
        public virtual async Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input)
        {
            var totalCount = await _currencyRepository.GetCountAsync(input.FilterText, input.Code, input.Name);
            var items = await _currencyRepository.GetListAsync(
             input.FilterText
            , input.Sorting
            , input.Code
            , input.Name
            , input.DatePassive
            , input.Number
            , input.IsPassive

            , input.ApprovalStatus

            , input.MaxResultCount
            , input.SkipCount
            );

            return new PagedResultDto<CurrencyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(items)
            };
        }




        public virtual async Task<CurrencyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Currency, CurrencyDto>(await _currencyRepository.GetAsync(id));
        }



        // [Authorize(MiniDefinitionPermissions.Currencies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _currencyRepository.DeleteAsync(id);
        }

        //[Authorize(MiniDefinitionPermissions.Currencies.Edit)]
        public virtual async Task<CurrencyDto> UpdateAsync(Guid id, CurrencyUpdateDto input)
        {

            var currency = await _currencyManager.UpdateAsync(
                id,
                input.Code,
                input.Name,
                input.DatePassive,
                input.Number,
                input.IsPassive,
                input.ApprovalStatus,
                input.ConcurrencyStamp
            );


            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }
    }
}