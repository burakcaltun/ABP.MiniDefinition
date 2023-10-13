using Definition.ExchangeRateEntries;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.Extensions.Localization;
using MiniDefinition.Cities.Passive;
using MiniDefinition.Currencies;
using MiniDefinition.Localization;
using MiniDefinition.Shared;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;
using Volo.Abp.Uow;

namespace MiniDefinition.ExchangeRateEntries
{
    public class ExchangeRateEntriesAppService : Abstract.ExchangeRateEntriesAppService, IExchangeRateEntriesAppService
    {
        private readonly IExchangeRateEntryRepository _exchangeRateEntryRepository;
        private readonly ExchangeRateEntryManager _exchangeRateEntryManager;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IStringLocalizer<MiniDefinitionResource> _localizer;
        private readonly IAsyncQueryableExecuter _asyncQueryableExecuter;

        public ExchangeRateEntriesAppService(
            
            IExchangeRateEntryRepository exchangeRateEntryRepository, 
            ExchangeRateEntryManager exchangeRateEntryManager,
            ICurrencyRepository currencyRepository,
            IAsyncQueryableExecuter asyncQueryableExecuter,
        IStringLocalizer<MiniDefinitionResource> localizer) 
            : base(exchangeRateEntryRepository, exchangeRateEntryManager)
        {
            _exchangeRateEntryManager = exchangeRateEntryManager;
            _exchangeRateEntryRepository = exchangeRateEntryRepository;
            _currencyRepository = currencyRepository;
            _localizer = localizer;
            _asyncQueryableExecuter = asyncQueryableExecuter;
        }

        public async Task<LoadResult> BPGetListExhangeRateEntry(DataSourceLoadOptions loadOptions)
        {
            var getExhangeRateEntry = await _exchangeRateEntryRepository.GetQueryableAsync();
            getExhangeRateEntry.Select(xc => new ExchangeRateEntryLookupDto
            {
                Id = xc.Id,
                CustomsCode = xc.CustomsCode,
                ForexBuying = xc.ForexBuying,
                ForexSelling = xc.ForexSelling,
                BanknoteBuying = xc.BanknoteBuying,
                BanknoteSelling = xc.BanknoteSelling,
                FreeBuyExchangeRate = xc.FreeBuyExchangeRate,
                FreeSellExchangeRate = xc.FreeSellExchangeRate,
                ExchangeRateEntryId = xc.ExchangeRateEntryId,
                CurrencyId = xc.CurrencyId
                
            });

            return await DataSourceLoader.LoadAsync(getExhangeRateEntry, loadOptions);
          
        }

        public async Task<ExchangeRateEntryLookupDto> BPGetExchangeRateEntryByID(Guid id)
        {
            var getExchange = await _exchangeRateEntryRepository.GetQueryableAsync();

            var exchange = (from xc in getExchange
                            where xc.Id == id
                            select new ExchangeRateEntryLookupDto
                            {
                                Id = xc.Id,
                                Date = xc.Date,
                                CustomsCode = xc.CustomsCode,
                                ForexBuying = xc.ForexBuying,
                                ForexSelling = xc.ForexSelling,
                                BanknoteBuying = xc.BanknoteBuying,
                                BanknoteSelling = xc.BanknoteSelling,
                                FreeBuyExchangeRate = xc.FreeBuyExchangeRate,
                                FreeSellExchangeRate = xc.FreeSellExchangeRate,
                                ExchangeRateEntryId = xc.ExchangeRateEntryId,
                                CurrencyId = xc.CurrencyId


                            }).FirstOrDefault();
                if(exchange == null)
            {
                throw new UserFriendlyException(string.Format(_localizer["STK:Message:Default:InformationDeleteRecordNotShow"]));
            }

            return exchange;
        }


        public async Task<ExchangeRateEntryDto> BPExchangeRateFieldNotBeLeftBlank(ExchangeRateEntryDto input)
        {
            var getCurrencies = await _currencyRepository.GetQueryableAsync();
            var currencies = (from c in getCurrencies
                              where  c.Id == input.CurrencyId 
                              select c.Number);

            if(input.CurrencyId == null)
            {
                throw new UserFriendlyException(_localizer["DEF:Message:Default:FieldCanNotBeLeftBlank"]);
            }
            if(input.Date == null)
            {
                throw new UserFriendlyException(_localizer["DEF:Message:Default:FieldCanNotBeLeftBlank"]);
            }
            if(input == null)
            {
                throw new UserFriendlyException(_localizer["DEF:Message:ExchangeRateEntries:MustEnterAtLeastOneExchangeRate"]);
            }

            return input;

        }

        public async Task<ExchangeRateEntryDto> BPDateAndCurrencyCodeCheck(ExchangeRateEntryDto input)
        {
            var xc = await _exchangeRateEntryRepository.GetQueryableAsync();
            var exchangeRates = xc.FirstOrDefault( x => 
                x.Id == input.Id &&
                x.CurrencyId == input.CurrencyId &&
                x.Date == input.Date);

            if (exchangeRates != null)
            {
                throw new UserFriendlyException(_localizer["DEF:Message:ExchangeRateEntriesShouldChangeDateOrCurrencyCode"]);
            }

            return input;
        }

        public async Task BPExchangeRateEntriesValidation(ExchangeRateEntryDto input)
        {
            await BPExchangeRateFieldNotBeLeftBlank(input);
            await BPDateAndCurrencyCodeCheck(input);
        }





        public async Task<ExchangeRateEntryDto> BPAddExchangeRateEntries(ExchangeRateEntryDto input)
        {
            await BPExchangeRateEntriesValidation(input);

            var xc = await _exchangeRateEntryManager.CreateAsync(
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

            return ObjectMapper.Map<ExchangeRateEntry, ExchangeRateEntryDto>(xc);

        }

        public async Task<ExchangeRateEntryDto> BPUpdateExchangeRateEntries(Guid id,  ExchangeRateEntryDto input)
        {
            await BPExchangeRateEntriesValidation(input);

            var xc = await _exchangeRateEntryManager.UpdateAsync(
                id,
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

            return ObjectMapper.Map<ExchangeRateEntry,ExchangeRateEntryDto>(xc);    
        }



        public async Task BPDeleteExchangeRateEntries(Guid id)
        {
            await _exchangeRateEntryRepository.DeleteAsync(id);
        }



        public async Task BPCentralBankDataFill()

        {


            XmlDocument currentExchangeXml = new XmlDocument();
            currentExchangeXml.Load("https://www.tcmb.gov.tr/kurlar/today.xml");
            XmlSerializer serializer = new XmlSerializer(typeof(ExchangeCurrenciesDto));
            StringReader stringReader = new StringReader(currentExchangeXml.InnerXml.Trim());
            XmlReader xmlReader = new XmlTextReader(stringReader);
            ExchangeCurrenciesDto exchangeRateTcmbDtos = (ExchangeCurrenciesDto)serializer.Deserialize(xmlReader);



            var currencyResults = await _currencyRepository.ToListAsync();

            foreach (var currency in currencyResults)
            {
                if (exchangeRateTcmbDtos != null)
                {
                    var exchangeRateQuery = (await _exchangeRateEntryRepository.GetQueryableAsync()).Where(x => x.Date == DateTime.Today.AddDays(1) && x.CurrencyId == currency.Id);
                    var exchanreRateResult = await _asyncQueryableExecuter.FirstOrDefaultAsync(exchangeRateQuery);
                    if (exchanreRateResult != null)
                    {
                        continue;
                    }
                    var currencyForTcmb = exchangeRateTcmbDtos.Currency.Find(x => x.CurrencyCode == currency.Code);
                    ExchangeRateEntryDto exchangeRateCreateDto = new ExchangeRateEntryDto();
                    exchangeRateCreateDto.CurrencyId = currency.Id;

                    exchangeRateQuery = (await _exchangeRateEntryRepository.GetQueryableAsync()).Where(x => x.Date == DateTime.Today && x.CurrencyId == currency.Id);
                    exchanreRateResult = await _asyncQueryableExecuter.FirstOrDefaultAsync(exchangeRateQuery);
                    if (exchanreRateResult == null)
                    {
                        exchangeRateCreateDto.Date = DateTime.Today;
                    }
                    else
                    {
                        exchangeRateCreateDto.Date = DateTime.Today.AddDays(1);
                    }
                    exchangeRateCreateDto.ForexBuying = Convert.ToDecimal(currencyForTcmb?.ForexBuying.ToString().Replace('.', ','));
                    exchangeRateCreateDto.ForexSelling = Convert.ToDecimal(currencyForTcmb?.ForexSelling.ToString().Replace('.', ','));
                    exchangeRateCreateDto.BanknoteBuying = Convert.ToDecimal(currencyForTcmb?.BanknoteBuying.ToString().Replace('.', ','));
                    exchangeRateCreateDto.BanknoteSelling = Convert.ToDecimal(currencyForTcmb?.BanknoteSelling.ToString().Replace('.', ','));
                    exchangeRateCreateDto.FreeBuyExchangeRate = 0;
                    exchangeRateCreateDto.FreeSellExchangeRate = 0;

                    await BPAddExchangeRateEntries(exchangeRateCreateDto);
                }
            }
        }


    }
}
