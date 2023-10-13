using System;

using AutoMapper;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using MiniDefinition.Infrastructure.Data;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Domain.Repositories.Interfaces;
using MiniDefinition.Dto;
using MiniDefinition.Configuration.AutoMapper;
using MiniDefinition.Test.Setup;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MiniDefinition.Test.Controllers
{
    public class ExchangeRateEntriesControllerIntTest
    {
        public ExchangeRateEntriesControllerIntTest()
        {
            _factory = new AppWebApplicationFactory<TestStartup>().WithMockUser();
            _client = _factory.CreateClient();

            _exchangeRateEntryRepository = _factory.GetRequiredService<IExchangeRateEntryRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = config.CreateMapper();

            InitTest();
        }

        private static readonly DateTime DefaultDate = DateTime.UnixEpoch;
        private static readonly DateTime UpdatedDate = DateTime.UtcNow;

        private static readonly int? DefaultCustomsCode = 1;
        private static readonly int? UpdatedCustomsCode = 2;

        private static readonly decimal? DefaultForexBuying = 1M;
        private static readonly decimal? UpdatedForexBuying = 2M;

        private static readonly decimal? DefaultForexSelling = 1M;
        private static readonly decimal? UpdatedForexSelling = 2M;

        private static readonly decimal? DefaultBanknoteBuying = 1M;
        private static readonly decimal? UpdatedBanknoteBuying = 2M;

        private static readonly decimal? DefaultBanknoteSelling = 1M;
        private static readonly decimal? UpdatedBanknoteSelling = 2M;

        private static readonly decimal? DefaultFreeBuyExchangeRate = 1M;
        private static readonly decimal? UpdatedFreeBuyExchangeRate = 2M;

        private static readonly decimal? DefaultFreeSellExchangeRate = 1M;
        private static readonly decimal? UpdatedFreeSellExchangeRate = 2M;

        private readonly AppWebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;
        private readonly IExchangeRateEntryRepository _exchangeRateEntryRepository;

        private ExchangeRateEntry _exchangeRateEntry;

        private readonly IMapper _mapper;

        private ExchangeRateEntry CreateEntity()
        {
            return new ExchangeRateEntry
            {
                Date = DefaultDate,
                CustomsCode = DefaultCustomsCode,
                ForexBuying = DefaultForexBuying,
                ForexSelling = DefaultForexSelling,
                BanknoteBuying = DefaultBanknoteBuying,
                BanknoteSelling = DefaultBanknoteSelling,
                FreeBuyExchangeRate = DefaultFreeBuyExchangeRate,
                FreeSellExchangeRate = DefaultFreeSellExchangeRate,
            };
        }

        private void InitTest()
        {
            _exchangeRateEntry = CreateEntity();
        }

        [Fact]
        public async Task CreateExchangeRateEntry()
        {
            var databaseSizeBeforeCreate = await _exchangeRateEntryRepository.CountAsync();

            // Create the ExchangeRateEntry
            ExchangeRateEntryDto _exchangeRateEntryDto = _mapper.Map<ExchangeRateEntryDto>(_exchangeRateEntry);
            var response = await _client.PostAsync("/api/exchange-rate-entries", TestUtil.ToJsonContent(_exchangeRateEntryDto));
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Validate the ExchangeRateEntry in the database
            var exchangeRateEntryList = await _exchangeRateEntryRepository.GetAllAsync();
            exchangeRateEntryList.Count().Should().Be(databaseSizeBeforeCreate + 1);
            var testExchangeRateEntry = exchangeRateEntryList.Last();
            testExchangeRateEntry.Date.Should().Be(DefaultDate);
            testExchangeRateEntry.CustomsCode.Should().Be(DefaultCustomsCode);
            testExchangeRateEntry.ForexBuying.Should().Be(DefaultForexBuying);
            testExchangeRateEntry.ForexSelling.Should().Be(DefaultForexSelling);
            testExchangeRateEntry.BanknoteBuying.Should().Be(DefaultBanknoteBuying);
            testExchangeRateEntry.BanknoteSelling.Should().Be(DefaultBanknoteSelling);
            testExchangeRateEntry.FreeBuyExchangeRate.Should().Be(DefaultFreeBuyExchangeRate);
            testExchangeRateEntry.FreeSellExchangeRate.Should().Be(DefaultFreeSellExchangeRate);
        }

        [Fact]
        public async Task CreateExchangeRateEntryWithExistingId()
        {
            var databaseSizeBeforeCreate = await _exchangeRateEntryRepository.CountAsync();
            // Create the ExchangeRateEntry with an existing ID
            _exchangeRateEntry.Id = 1L;

            // An entity with an existing ID cannot be created, so this API call must fail
            ExchangeRateEntryDto _exchangeRateEntryDto = _mapper.Map<ExchangeRateEntryDto>(_exchangeRateEntry);
            var response = await _client.PostAsync("/api/exchange-rate-entries", TestUtil.ToJsonContent(_exchangeRateEntryDto));

            // Validate the ExchangeRateEntry in the database
            var exchangeRateEntryList = await _exchangeRateEntryRepository.GetAllAsync();
            exchangeRateEntryList.Count().Should().Be(databaseSizeBeforeCreate);
        }

        [Fact]
        public async Task GetAllExchangeRateEntries()
        {
            // Initialize the database
            await _exchangeRateEntryRepository.CreateOrUpdateAsync(_exchangeRateEntry);
            await _exchangeRateEntryRepository.SaveChangesAsync();

            // Get all the exchangeRateEntryList
            var response = await _client.GetAsync("/api/exchange-rate-entries?sort=id,desc");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.[*].id").Should().Contain(_exchangeRateEntry.Id);
            json.SelectTokens("$.[*].date").Should().Contain(DefaultDate);
            json.SelectTokens("$.[*].customsCode").Should().Contain(DefaultCustomsCode);
            json.SelectTokens("$.[*].forexBuying").Should().Contain(DefaultForexBuying);
            json.SelectTokens("$.[*].forexSelling").Should().Contain(DefaultForexSelling);
            json.SelectTokens("$.[*].banknoteBuying").Should().Contain(DefaultBanknoteBuying);
            json.SelectTokens("$.[*].banknoteSelling").Should().Contain(DefaultBanknoteSelling);
            json.SelectTokens("$.[*].freeBuyExchangeRate").Should().Contain(DefaultFreeBuyExchangeRate);
            json.SelectTokens("$.[*].freeSellExchangeRate").Should().Contain(DefaultFreeSellExchangeRate);
        }

        [Fact]
        public async Task GetExchangeRateEntry()
        {
            // Initialize the database
            await _exchangeRateEntryRepository.CreateOrUpdateAsync(_exchangeRateEntry);
            await _exchangeRateEntryRepository.SaveChangesAsync();

            // Get the exchangeRateEntry
            var response = await _client.GetAsync($"/api/exchange-rate-entries/{_exchangeRateEntry.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.id").Should().Contain(_exchangeRateEntry.Id);
            json.SelectTokens("$.date").Should().Contain(DefaultDate);
            json.SelectTokens("$.customsCode").Should().Contain(DefaultCustomsCode);
            json.SelectTokens("$.forexBuying").Should().Contain(DefaultForexBuying);
            json.SelectTokens("$.forexSelling").Should().Contain(DefaultForexSelling);
            json.SelectTokens("$.banknoteBuying").Should().Contain(DefaultBanknoteBuying);
            json.SelectTokens("$.banknoteSelling").Should().Contain(DefaultBanknoteSelling);
            json.SelectTokens("$.freeBuyExchangeRate").Should().Contain(DefaultFreeBuyExchangeRate);
            json.SelectTokens("$.freeSellExchangeRate").Should().Contain(DefaultFreeSellExchangeRate);
        }

        [Fact]
        public async Task GetNonExistingExchangeRateEntry()
        {
            var maxValue = long.MaxValue;
            var response = await _client.GetAsync("/api/exchange-rate-entries/" + maxValue);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateExchangeRateEntry()
        {
            // Initialize the database
            await _exchangeRateEntryRepository.CreateOrUpdateAsync(_exchangeRateEntry);
            await _exchangeRateEntryRepository.SaveChangesAsync();
            var databaseSizeBeforeUpdate = await _exchangeRateEntryRepository.CountAsync();

            // Update the exchangeRateEntry
            var updatedExchangeRateEntry = await _exchangeRateEntryRepository.QueryHelper().GetOneAsync(it => it.Id == _exchangeRateEntry.Id);
            // Disconnect from session so that the updates on updatedExchangeRateEntry are not directly saved in db
            //TODO detach
            updatedExchangeRateEntry.Date = UpdatedDate;
            updatedExchangeRateEntry.CustomsCode = UpdatedCustomsCode;
            updatedExchangeRateEntry.ForexBuying = UpdatedForexBuying;
            updatedExchangeRateEntry.ForexSelling = UpdatedForexSelling;
            updatedExchangeRateEntry.BanknoteBuying = UpdatedBanknoteBuying;
            updatedExchangeRateEntry.BanknoteSelling = UpdatedBanknoteSelling;
            updatedExchangeRateEntry.FreeBuyExchangeRate = UpdatedFreeBuyExchangeRate;
            updatedExchangeRateEntry.FreeSellExchangeRate = UpdatedFreeSellExchangeRate;

            ExchangeRateEntryDto updatedExchangeRateEntryDto = _mapper.Map<ExchangeRateEntryDto>(updatedExchangeRateEntry);
            var response = await _client.PutAsync($"/api/exchange-rate-entries/{_exchangeRateEntry.Id}", TestUtil.ToJsonContent(updatedExchangeRateEntryDto));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Validate the ExchangeRateEntry in the database
            var exchangeRateEntryList = await _exchangeRateEntryRepository.GetAllAsync();
            exchangeRateEntryList.Count().Should().Be(databaseSizeBeforeUpdate);
            var testExchangeRateEntry = exchangeRateEntryList.Last();
            testExchangeRateEntry.Date.Should().BeCloseTo(UpdatedDate, 1.Milliseconds());
            testExchangeRateEntry.CustomsCode.Should().Be(UpdatedCustomsCode);
            testExchangeRateEntry.ForexBuying.Should().Be(UpdatedForexBuying);
            testExchangeRateEntry.ForexSelling.Should().Be(UpdatedForexSelling);
            testExchangeRateEntry.BanknoteBuying.Should().Be(UpdatedBanknoteBuying);
            testExchangeRateEntry.BanknoteSelling.Should().Be(UpdatedBanknoteSelling);
            testExchangeRateEntry.FreeBuyExchangeRate.Should().Be(UpdatedFreeBuyExchangeRate);
            testExchangeRateEntry.FreeSellExchangeRate.Should().Be(UpdatedFreeSellExchangeRate);
        }

        [Fact]
        public async Task UpdateNonExistingExchangeRateEntry()
        {
            var databaseSizeBeforeUpdate = await _exchangeRateEntryRepository.CountAsync();

            // If the entity doesn't have an ID, it will throw BadRequestAlertException
            ExchangeRateEntryDto _exchangeRateEntryDto = _mapper.Map<ExchangeRateEntryDto>(_exchangeRateEntry);
            var response = await _client.PutAsync("/api/exchange-rate-entries/1", TestUtil.ToJsonContent(_exchangeRateEntryDto));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the ExchangeRateEntry in the database
            var exchangeRateEntryList = await _exchangeRateEntryRepository.GetAllAsync();
            exchangeRateEntryList.Count().Should().Be(databaseSizeBeforeUpdate);
        }

        [Fact]
        public async Task DeleteExchangeRateEntry()
        {
            // Initialize the database
            await _exchangeRateEntryRepository.CreateOrUpdateAsync(_exchangeRateEntry);
            await _exchangeRateEntryRepository.SaveChangesAsync();
            var databaseSizeBeforeDelete = await _exchangeRateEntryRepository.CountAsync();

            var response = await _client.DeleteAsync($"/api/exchange-rate-entries/{_exchangeRateEntry.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Validate the database is empty
            var exchangeRateEntryList = await _exchangeRateEntryRepository.GetAllAsync();
            exchangeRateEntryList.Count().Should().Be(databaseSizeBeforeDelete - 1);
        }

        [Fact]
        public void EqualsVerifier()
        {
            TestUtil.EqualsVerifier(typeof(ExchangeRateEntry));
            var exchangeRateEntry1 = new ExchangeRateEntry
            {
                Id = 1L
            };
            var exchangeRateEntry2 = new ExchangeRateEntry
            {
                Id = exchangeRateEntry1.Id
            };
            exchangeRateEntry1.Should().Be(exchangeRateEntry2);
            exchangeRateEntry2.Id = 2L;
            exchangeRateEntry1.Should().NotBe(exchangeRateEntry2);
            exchangeRateEntry1.Id = 0;
            exchangeRateEntry1.Should().NotBe(exchangeRateEntry2);
        }
    }
}
