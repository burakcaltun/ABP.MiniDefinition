using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MiniDefinition.ExchangeRateEntries;
using MiniDefinition.Localization;
using MiniDefinition.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDefinition.Controllers.ExchangeRateEntries
{
    public class ExchangeRateEntriesController : Abstract.ExchangeRateEntriesController, IExchangeRateEntriesAppService
    {
        private readonly IExchangeRateEntriesAppService _exchangeRateEntriesAppService;
        private readonly IExchangeRateEntryRepository _exchangeRateEntryRepository;
        private readonly IStringLocalizer<MiniDefinitionResource> _localizer;

        public ExchangeRateEntriesController(
            IExchangeRateEntriesAppService exchangeRateEntriesAppService,
            IExchangeRateEntryRepository exchangeRateEntryRepository,
            IStringLocalizer<MiniDefinitionResource> localizer)
            : base(exchangeRateEntriesAppService)
        {
            _exchangeRateEntriesAppService = exchangeRateEntriesAppService;
            _exchangeRateEntryRepository = exchangeRateEntryRepository;
            _localizer = localizer;
        }


        //[HttpGet]
        //[Route("bp-get-list-payments/{isActive}")]
        //public async Task<LoadResult> BPGetListPayments(Guid userId, SalePurchaseEnum salePurchaseEnum, string? isActive, DataSourceLoadOptions loadOptions, Guid? language, Guid? secondlanguage)
        //{
        //    return await _countriesAppService.BPGetListPayments(userId, salePurchaseEnum, isActive, loadOptions, language, secondlanguage);
        //}

        [HttpGet]
        [Route("bp-get-list-exchange-rate-entry")]
        public async Task<LoadResult> BPGetListExhangeRateEntry(DataSourceLoadOptions loadOptions)
        {
            return await _exchangeRateEntriesAppService.BPGetListExhangeRateEntry(loadOptions);

        }

        [HttpGet]
        [Route("bp-get-exchange-rate-entry-by-id/{id}")]
        public async Task<ExchangeRateEntryLookupDto> BPGetExchangeRateEntryByID(Guid id)
        {
            return await _exchangeRateEntriesAppService.BPGetExchangeRateEntryByID(id);
        }

        [HttpPost]
        [Route("bp-add-exchange-rate-entries")]
        public async Task<ExchangeRateEntryDto> BPAddExchangeRateEntries(ExchangeRateEntryDto input)
        {
            return await _exchangeRateEntriesAppService.BPAddExchangeRateEntries(input);
        }

        [HttpPut]
        [Route("bp-update-exchange-rate-entries/{id}")]
        public async Task<ExchangeRateEntryDto> BPUpdateExchangeRateEntries(Guid id, ExchangeRateEntryDto input)
        {
            return await _exchangeRateEntriesAppService.BPUpdateExchangeRateEntries(id, input);
        }


        [HttpDelete]
        [Route("bp-delete-exchange-rate-entries{id}")]
        public async Task BPDeleteExchangeRateEntries(Guid id)
        {
           await _exchangeRateEntriesAppService.BPDeleteExchangeRateEntries(id);
        }

        [HttpGet]
        [Route("fill-central-bank-data")]
        public async Task BPCentralBankDataFill()
        {
            await _exchangeRateEntriesAppService.BPCentralBankDataFill();
        }


    }
}
